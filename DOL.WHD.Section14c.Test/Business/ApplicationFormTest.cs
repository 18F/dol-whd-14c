using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Business;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class ApplicationFormTest
    {
        public ApplicationFormTest()
        {
        }

    }
}
