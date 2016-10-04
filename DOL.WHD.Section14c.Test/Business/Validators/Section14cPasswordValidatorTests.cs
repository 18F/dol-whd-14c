using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Business.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOL.WHD.Section14c.Test.Business.Validators
{
    [TestClass]
    public class Section14cPasswordValidatorTests
    {
        [TestMethod]
        public async Task ValidatesPasswordLength()
        {
            var validator = new Section14cPasswordValidator
            {
                RequiredLength = 8
            };

            var shortResult = await validator.ValidateAsync("f8E9bj");
            Assert.IsFalse(shortResult.Succeeded);

            var longResult = await validator.ValidateAsync("asfd09j1nlja");
            Assert.IsTrue(longResult.Succeeded);
        }

        [TestMethod]
        public async Task ValidatesUppercase()
        {
            var validator = new Section14cPasswordValidator()
            {
                RequireUppercase = true
            };

            var missingUppercaseResult = await validator.ValidateAsync("asdflk12*mml");
            Assert.IsTrue(missingUppercaseResult.Errors.Contains("Passwords must have at least one uppercase ('A'-'Z')."));

            var containsUppercaseResult = await validator.ValidateAsync("asDflk12*mml");
            Assert.IsTrue(containsUppercaseResult.Succeeded);
        }

        [TestMethod]
        public async Task ValidatesLowercase()
        {
            var validator = new Section14cPasswordValidator()
            {
                RequireLowercase = true
            };

            var missingLowercaseResult = await validator.ValidateAsync("ASDFKLG12*MML");
            Assert.IsTrue(missingLowercaseResult.Errors.Contains("Passwords must have at least one lowercase ('a'-'z')."));

            var containsLowercaseResult = await validator.ValidateAsync("asDflk12*mml");
            Assert.IsTrue(containsLowercaseResult.Succeeded);
        }

        [TestMethod]
        public async Task ValidatesNumbers()
        {
            var validator = new Section14cPasswordValidator()
            {
                RequireDigit = true
            };

            var missingNumberResult = await validator.ValidateAsync("ASDFKLG*MML");
            Assert.IsTrue(missingNumberResult.Errors.Contains("Passwords must have at least one digit ('0'-'9')."));

            var containsNumberResult = await validator.ValidateAsync("asDflk12*mml");
            Assert.IsTrue(containsNumberResult.Succeeded);
        }

        [TestMethod]
        public async Task ValidatesSpecialCharacter()
        {
            var validator = new Section14cPasswordValidator()
            {
                RequireNonLetterOrDigit = true
            };

            var missingSpecialCharacterResult = await validator.ValidateAsync("ASDFKLGMML");
            Assert.IsTrue(missingSpecialCharacterResult.Errors.Contains("Passwords must have at least one non letter or digit character."));

            var containsSpecialCharacterResult = await validator.ValidateAsync("asDflk12*mml");
            Assert.IsTrue(containsSpecialCharacterResult.Succeeded);
        }

        [TestMethod]
        public async Task ValidatesDictionaryWords()
        {
            var validator = new Section14cPasswordValidator
            {
                RequireZxcvbn = true
            };

            var dictionaryResult = await validator.ValidateAsync("Football2005!");
            Assert.IsTrue(dictionaryResult.Errors.Contains("Password does not meet complexity requirements."));
        }

        [TestMethod]
        public async Task ValidatesConsecutiveCharacterStrings()
        {
            var validator = new Section14cPasswordValidator
            {
                RequireZxcvbn = true
            };

            var consecutiveCharactersResult = await validator.ValidateAsync("abcdefg1000!");
            Assert.IsTrue(consecutiveCharactersResult.Errors.Contains("Password does not meet complexity requirements."));
        }

        [TestMethod]
        public async Task ValidatesKeyboardPatterns()
        {
            var validator = new Section14cPasswordValidator
            {
                RequireZxcvbn = true
            };

            var keyboardPatternsResult = await validator.ValidateAsync("qwertyuiop&*18");
            Assert.IsTrue(keyboardPatternsResult.Errors.Contains("Password does not meet complexity requirements."));
        }

        [TestMethod]
        public async Task ValidatesGenericPasswords()
        {
            var validator = new Section14cPasswordValidator
            {
                RequireZxcvbn = true
            };

            var genericPasswordResult = await validator.ValidateAsync("P@ssw0rd1");
            Assert.IsTrue(genericPasswordResult.Errors.Contains("Password does not meet complexity requirements."));
        }
    }
}
