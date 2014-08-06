using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using XIMALAYA.PCDesktop.Core.ParamsModel;

namespace XIMALAYA.PCDesktop.NUnitTest
{
    [TestFixture]
    public class TestBaseParam
    {
        [Test]
        public void TestBaseParamCase1()
        {
            var baseParam = new BaseParam
            {
                Device = DeviceType.pc,
                Page = 1,
                PerPage = 20
            };

            Assert.AreEqual(baseParam.ToString(), "device=pc&page=1&per_page=20");
            baseParam = new BaseParam
            {
                Device = DeviceType.android,
                Page = 1,
                PerPage = 20
            };
            Assert.AreEqual(baseParam.ToString(), "device=android&page=1&per_page=20");
            baseParam = new BaseParam
            {
                Device = DeviceType.android
            };
            Assert.AreEqual(baseParam.ToString(), "device=android");
        }
    }
}
