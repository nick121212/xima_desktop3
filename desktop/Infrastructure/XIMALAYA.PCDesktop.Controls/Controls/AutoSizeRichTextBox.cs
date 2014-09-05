using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace XIMALAYA.PCDesktop.Controls
{
    public class AutoSizeRichTextBox : RichTextBox
    {
        private readonly DispatcherTimer DispatcherTimer = new DispatcherTimer();

        public double ContainerWidth
        {
            get { return (double)GetValue(ContainerWidthProperty); }
            set { SetValue(ContainerWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContainerWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContainerWidthProperty =
            DependencyProperty.Register("ContainerWidth", typeof(double), typeof(AutoSizeRichTextBox), new PropertyMetadata(0D));


        public double PaddingLeftFixed
        {
            get { return (double)GetValue(PaddingLeftFixedProperty); }
            set { SetValue(PaddingLeftFixedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PaddingLeftFixed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaddingLeftFixedProperty =
            DependencyProperty.Register("PaddingLeftFixed", typeof(double), typeof(AutoSizeRichTextBox), new PropertyMetadata(-1D));


        public AutoSizeRichTextBox()
        {
            Height = Double.NaN;//set to nan to enable auto-height
            Loaded += ((sender, args) => AdjustSizeByConent());

            DispatcherTimer.Interval = TimeSpan.FromMilliseconds(50);
            DispatcherTimer.Tick += new EventHandler((o, e) =>
            {
                if (Math.Abs(this.Margin.Left) >= this.Width - this.ContainerWidth || this.Margin.Left == 5)
                {
                    this.PaddingLeftFixed *= -1;
                }
                this.Margin = new Thickness(this.Margin.Left + this.PaddingLeftFixed, this.Margin.Top, this.Margin.Right, this.Margin.Bottom);
            });
            DispatcherTimer.IsEnabled = false;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            DispatcherTimer.IsEnabled = false;
            AdjustSizeByConent();
        }

        public void AdjustSizeByConent()
        {
            var formattedText = GetFormattedText(Document);
            // ReSharper disable ConvertToConstant.Local
            var remainW = 20;
            // ReSharper restore ConvertToConstant.Local

            Width = Math.Min(MaxWidth, Math.Max(MinWidth, formattedText.WidthIncludingTrailingWhitespace + remainW));

            if (this.ContainerWidth > 0 && Width > this.ContainerWidth + 5)
            {
                DispatcherTimer.IsEnabled = true;
            }
            else
            {
                DispatcherTimer.IsEnabled = false;
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
