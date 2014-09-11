using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace XIMALAYA.PCDesktop.Controls
{
    public class AutoSizeRichTextBox : RichTextBox
    {
        private Storyboard StoryBoard = new Storyboard();

        public double ContainerWidth
        {
            get { return (double)GetValue(ContainerWidthProperty); }
            set { SetValue(ContainerWidthProperty, value); }
        }
        public static readonly DependencyProperty ContainerWidthProperty =
            DependencyProperty.Register("ContainerWidth", typeof(double), typeof(AutoSizeRichTextBox), new PropertyMetadata(0D));
        public AutoSizeRichTextBox()
        {
            this.Height = Double.NaN;
            this.StoryBoard = new Storyboard();
            this.Loaded += ((sender, args) => AdjustSizeByConent());
            this.Unloaded += ((sender, args) => {
                this.StoryBoard.Stop();
            });
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            AdjustSizeByConent();
        }
        private void StoryBoardInit(double left)
        {
            QuinticEase SineEase = new QuinticEase { EasingMode = EasingMode.EaseIn };
            ThicknessAnimationUsingKeyFrames keyFrames = new ThicknessAnimationUsingKeyFrames();
            EasingThicknessKeyFrame keyFrame = new EasingThicknessKeyFrame(new Thickness(left, this.Margin.Top, this.Margin.Right, this.Margin.Bottom), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3)), SineEase);
            EasingThicknessKeyFrame keyFrame1 = new EasingThicknessKeyFrame(new Thickness(0, this.Margin.Top, this.Margin.Right, this.Margin.Bottom), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(6)), SineEase);

            this.Margin = new Thickness(0,this.Margin.Top,this.Margin.Right,this.Margin.Bottom);
            keyFrames.RepeatBehavior = RepeatBehavior.Forever;
            StoryBoard.Children.Clear();
            keyFrames.KeyFrames.Add(keyFrame);
            keyFrames.KeyFrames.Add(keyFrame1);
            StoryBoard.Children.Add(keyFrames);
            Storyboard.SetTargetProperty(keyFrames, new PropertyPath("(FrameworkElement.Margin)"));
            Storyboard.SetTarget(keyFrames, this);

            StoryBoard.FillBehavior = FillBehavior.HoldEnd;
            StoryBoard.Begin();
        }
        public void AdjustSizeByConent()
        {
            var formattedText = GetFormattedText(Document);
            // ReSharper disable ConvertToConstant.Local
            var remainW = 20;
            // ReSharper restore ConvertToConstant.Local

            Width = Math.Min(MaxWidth, Math.Max(MinWidth, formattedText.WidthIncludingTrailingWhitespace + remainW));

            StoryBoard.Stop();
            if (this.ContainerWidth > 0 && Width > this.ContainerWidth + 5)
            {
                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                this.StoryBoardInit(this.ContainerWidth - Width);
            }
            else
            {
                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            }
        }
        private static FormattedText GetFormattedText(FlowDocument doc)
        {
            var output = new FormattedText(
                GetText(doc),
                CultureInfo.CurrentCulture,
                doc.FlowDirection,
                new Typeface(doc.FontFamily, doc.FontStyle, doc.FontWeight, doc.FontStretch),
                doc.FontSize,
                doc.Foreground);

            int offset = 0;

            foreach (TextElement textElement in GetRunsAndParagraphs(doc))
            {
                var run = textElement as Run;

                if (run != null)
                {
                    int count = run.Text.Length;

                    output.SetFontFamily(run.FontFamily, offset, count);
                    output.SetFontSize(run.FontSize, offset, count);
                    output.SetFontStretch(run.FontStretch, offset, count);
                    output.SetFontStyle(run.FontStyle, offset, count);
                    output.SetFontWeight(run.FontWeight, offset, count);
                    output.SetForegroundBrush(run.Foreground, offset, count);
                    output.SetTextDecorations(run.TextDecorations, offset, count);

                    offset += count;
                }
                else
                {
                    offset += Environment.NewLine.Length;
                }
            }

            return output;
        }
        private static IEnumerable<TextElement> GetRunsAndParagraphs(FlowDocument doc)
        {
            for (TextPointer position = doc.ContentStart;
                position != null && position.CompareTo(doc.ContentEnd) <= 0;
                position = position.GetNextContextPosition(LogicalDirection.Forward))
            {
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.ElementEnd)
                {
                    var run = position.Parent as Run;

                    if (run != null)
                    {
                        yield return run;
                    }
                    else
                    {
                        var para = position.Parent as Paragraph;

                        if (para != null)
                        {
                            yield return para;
                        }
                        else
                        {
                            var lineBreak = position.Parent as LineBreak;

                            if (lineBreak != null)
                            {
                                yield return lineBreak;
                            }
                        }
                    }
                }
            }
        }
        private static string GetText(FlowDocument doc)
        {
            var sb = new StringBuilder();

            foreach (TextElement text in GetRunsAndParagraphs(doc))
            {
                var run = text as Run;
                sb.Append(run == null ? Environment.NewLine : run.Text);
            }

            return sb.ToString();
        }
    }
}
