using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Fact]
        public void RetornaPrecoAtualizadoQuandoAplicaDesconto()
        {
            //arrange
            Rota rota = new Rota("OrigemA", "DestinoB");
            Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
            double precoOriginal = 100.00;
            double desconto = 20.00;
            double precoComDesconto = precoOriginal - desconto;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            //act
            oferta.Desconto = desconto;

            //assert
            Assert.Equal(precoComDesconto, oferta.Preco);
        }


        [Theory]
        [InlineData(120, 30)]
        [InlineData(100, 30)]
        public void RetornaDescontoMaximoquandoDescontoForMaiorOuIgualAoPreco(double desconto, double precoComDesconto)
        {
            //arrange
            Rota rota = new Rota("OrigemA", "DestinoB");
            Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
            double precoOriginal = 100.00;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            //act
            oferta.Desconto = desconto;

            //assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

        [Fact]
        public void RetornaTresErrosDeValidacaoSePrecoPeriodoERotaSaoInvalidos()
        {
            //arrange
            int quantidadeDeErros = 3;
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 4, 30));
            double preco = -120;

            //act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            
            //assert
            Assert.Equal(quantidadeDeErros, oferta.Erros.Count());
        }
    }
}
