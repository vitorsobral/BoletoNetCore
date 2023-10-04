using System;
using NUnit.Framework;

namespace BoletoNetCore.Testes;

[TestFixture]
[System.ComponentModel.Category("Bradesco Carteira 16")]
public class BancoBradescoCarteira16
{
    readonly IBanco _banco;
    public BancoBradescoCarteira16()
    {
        var contaBancaria = new ContaBancaria
        {
            Agencia = "1234",
            DigitoAgencia = "X",
            Conta = "123456",
            DigitoConta = "X",
            CarteiraPadrao = "16",
            TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
            TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
            TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
        };
        _banco = Banco.Instancia(Bancos.Bradesco);
        _banco.Beneficiario = TestUtils.GerarBeneficiario("1411213", "", "", contaBancaria);
        _banco.FormataBeneficiario();
    }

    [Test]
    public void Bradesco_16_REM240()
    {
        TestUtils.TestarHomologacao(_banco, TipoArquivo.CNAB240, nameof(BancoBradescoCarteira16), 5, true, "?", 223344);
    }

    [Test]
    public void Bradesco_16_REM400()
    {
        TestUtils.TestarHomologacao(_banco, TipoArquivo.CNAB400, nameof(BancoBradescoCarteira16), 5, true, "?", 223344);
    }

    [TestCase(141.50, "453", "BB943A", "8", "016/00000000453-4", "23791690400000141501234090000000045301234560", "23791.23413 60000.000046 53012.345608 8 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "1", "016/00000000456-P", "23792693400002717161234090000000045601234560", "23791.23413 60000.000046 56012.345601 1 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "1", "016/00000000444-8", "23793690500000297211234090000000044401234560", "23791.23413 60000.000046 44012.345607 1 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "2", "016/00000000468-0", "23794693500000297211234090000000046801234560", "23791.23413 60000.000046 68012.345606 2 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "3", "016/00000000443-2", "23795690500000297211234090000000044301234560", "23791.23413 60000.000046 43012.345609 3 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "4", "016/00000000414-9", "23796687300000649391234090000000041401234560", "23791.23413 60000.000046 14012.345600 4 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "5", "016/00000000561-7", "23797702600000270001234090000000056101234560", "23791.23413 60000.000053 61012.345601 5 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "6", "016/00000000445-9", "23798690500002924111234090000000044501234560", "23791.23413 60000.000046 45012.345604 6 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "7", "016/00000000562-5", "23799702600000830001234090000000056201234560", "23791.23413 60000.000053 62012.345609 7 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_16_com_linha_digitavel_valida(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
    {
        var boleto = new Boleto(_banco)
        {
            DataVencimento = new DateTime(anoMesDia[0], anoMesDia[1], anoMesDia[2]),
            ValorTitulo = valorTitulo,
            NossoNumero = nossoNumero,
            NumeroDocumento = numeroDocumento,
            EspecieDocumento = TipoEspecieDocumento.DM,
            Pagador = TestUtils.GerarPagador()
        };

        boleto.ValidarDados();

        Assert.That(boleto.CodigoBarra.LinhaDigitavel, Is.EqualTo(linhaDigitavel), "Linha digitável inválida");
    }

    [TestCase(141.50, "453", "BB943A", "8", "016/00000000453-4", "23791690400000141501234090000000045301234560", "23791.23413 60000.000046 53012.345608 8 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "1", "016/00000000456-P", "23792693400002717161234090000000045601234560", "23791.23413 60000.000046 56012.345601 1 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "1", "016/00000000444-8", "23793690500000297211234090000000044401234560", "23791.23413 60000.000046 44012.345607 1 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "2", "016/00000000468-0", "23794693500000297211234090000000046801234560", "23791.23413 60000.000046 68012.345606 2 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "3", "016/00000000443-2", "23795690500000297211234090000000044301234560", "23791.23413 60000.000046 43012.345609 3 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "4", "016/00000000414-9", "23796687300000649391234090000000041401234560", "23791.23413 60000.000046 14012.345600 4 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "5", "016/00000000561-7", "23797702600000270001234090000000056101234560", "23791.23413 60000.000053 61012.345601 5 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "6", "016/00000000445-9", "23798690500002924111234090000000044501234560", "23791.23413 60000.000046 45012.345604 6 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "7", "016/00000000562-5", "23799702600000830001234090000000056201234560", "23791.23413 60000.000053 62012.345609 7 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_16_com_digito_verificador_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
    {
        var boleto = new Boleto(_banco)
        {
            DataVencimento = new DateTime(anoMesDia[0], anoMesDia[1], anoMesDia[2]),
            ValorTitulo = valorTitulo,
            NossoNumero = nossoNumero,
            NumeroDocumento = numeroDocumento,
            EspecieDocumento = TipoEspecieDocumento.DM,
            Pagador = TestUtils.GerarPagador()
        };

        boleto.ValidarDados();

        Assert.That(boleto.CodigoBarra.DigitoVerificador, Is.EqualTo(digitoVerificador), $"Dígito Verificador diferente de {digitoVerificador}");
    }

    [TestCase(141.50, "453", "BB943A", "8", "016/00000000453-7", "23791690400000141501234090000000045301234560", "23791.23413 60000.000046 53012.345608 8 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "1", "016/00000000456-1", "23792693400002717161234090000000045601234560", "23791.23413 60000.000046 56012.345601 1 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "1", "016/00000000444-8", "23793690500000297211234090000000044401234560", "23791.23413 60000.000046 44012.345607 1 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "2", "016/00000000468-5", "23794693500000297211234090000000046801234560", "23791.23413 60000.000046 68012.345606 2 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "3", "016/00000000443-P", "23795690500000297211234090000000044301234560", "23791.23413 60000.000046 43012.345609 3 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "4", "016/00000000414-6", "23796687300000649391234090000000041401234560", "23791.23413 60000.000046 14012.345600 4 68730000064939", 2016, 8, 1)]
    [TestCase(2924.11, "445", "BB874A", "6", "016/00000000445-6", "23798690500002924111234090000000044501234560", "23791.23413 60000.000046 45012.345604 6 69050000292411", 2016, 9, 2)]
    [TestCase(270, "561", "BB932A", "5", "016/00000000561-4", "23797702600000270001234090000000056101234560", "23791.23413 60000.000053 61012.345601 5 70260000027000", 2017, 1, 1)]
    [TestCase(830, "562", "BB933A", "7", "016/00000000562-2", "23799702600000830001234090000000056201234560", "23791.23413 60000.000053 62012.345609 7 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_16_com_nosso_numero_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
    {
        var boleto = new Boleto(_banco)
        {
            DataVencimento = new DateTime(anoMesDia[0], anoMesDia[1], anoMesDia[2]),
            ValorTitulo = valorTitulo,
            NossoNumero = nossoNumero,
            NumeroDocumento = numeroDocumento,
            EspecieDocumento = TipoEspecieDocumento.DM,
            Pagador = TestUtils.GerarPagador()
        };

        boleto.ValidarDados();

        Assert.That(boleto.NossoNumeroFormatado, Is.EqualTo(nossoNumeroFormatado), "Nosso número inválido");
    }


    [TestCase(141.50, "453", "BB943A", "8", "016/00000000453-7", "23798690400000141501234160000000045301234560", "23791.23413 60000.000046 53012.345608 8 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "1", "016/00000000456-1", "23791693400002717161234160000000045601234560", "23791.23413 60000.000046 56012.345601 1 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "1", "016/00000000444-8", "23791690500000297211234160000000044401234560", "23791.23413 60000.000046 44012.345607 1 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "2", "016/00000000468-5", "23792693500000297211234160000000046801234560", "23791.23413 60000.000046 68012.345606 2 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "3", "016/00000000443-P", "23793690500000297211234160000000044301234560", "23791.23413 60000.000046 43012.345609 3 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "4", "016/00000000414-6", "23794687300000649391234160000000041401234560", "23791.23413 60000.000046 14012.345600 4 68730000064939", 2016, 8, 1)]
    [TestCase(2924.11, "445", "BB874A", "6", "016/00000000445-6", "23796690500002924111234160000000044501234560", "23791.23413 60000.000046 45012.345604 6 69050000292411", 2016, 9, 2)]
    [TestCase(270, "561", "BB932A", "5", "016/00000000561-4", "23795702600000270001234160000000056101234560", "23791.23413 60000.000053 61012.345601 5 70260000027000", 2017, 1, 1)]
    [TestCase(830, "562", "BB933A", "7", "016/00000000562-2", "23797702600000830001234160000000056201234560", "23791.23413 60000.000053 62012.345609 7 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_16_com_codigo_de_barras_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
    {
        var boleto = new Boleto(_banco)
        {
            DataVencimento = new DateTime(anoMesDia[0], anoMesDia[1], anoMesDia[2]),
            ValorTitulo = valorTitulo,
            NossoNumero = nossoNumero,
            NumeroDocumento = numeroDocumento,
            EspecieDocumento = TipoEspecieDocumento.DM,
            Pagador = TestUtils.GerarPagador()
        };

        boleto.ValidarDados();

        Assert.That(boleto.CodigoBarra.CodigoDeBarras, Is.EqualTo(codigoDeBarras), "Código de Barra inválido");
    }
}