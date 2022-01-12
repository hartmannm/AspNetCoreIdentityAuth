using ANCIA.ApiCore.Controllers.Models;
using System.Collections.Generic;
using Xunit;

namespace ANCIA.ApiCoreTests.Controllers.Models
{
    public class DefaultResultTest
    {
        [Fact]
        public void Deve_CriarResultadoSucesso_Quando_InformadoTrueConstrutor()
        {
            var result = new DefaultResult(true);
            Assert.True(result.Success);
        }

        [Fact]
        public void Deve_CriarResultadoErro_Quando_InformadoFalseConstrutor()
        {
            var result = new DefaultResult(false);
            Assert.False(result.Success);
        }

        [Fact]
        public void Deve_CriarResultadoSucesso_Quando_UtilizadoMetodoOk()
        {
            var result = DefaultResult.Ok("teste");
            Assert.True(result.Success);
        }

        [Fact]
        public void Deve_CriarResultadoErro_Quando_UtilizadoMetodoFail()
        {
            var errors = new List<string>();
            errors.Add("teste1");
            errors.Add("teste1");
            var result = DefaultResult.Fail(errors);
            Assert.False(result.Success);
        }
    }
}