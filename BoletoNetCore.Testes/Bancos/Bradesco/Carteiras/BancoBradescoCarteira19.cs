using System;
using NUnit.Framework;

namespace BoletoNetCore.Testes;

[TestFixture]
[System.ComponentModel.Category("Bradesco Carteira 19")]
public class BancoBradescoCarteira19
{
    readonly IBanco _banco;
    public BancoBradescoCarteira19()
    {
        var contaBancaria = new ContaBancaria
        {
            Agencia = "1234",
            DigitoAgencia = "X",
            Conta = "123456",
            DigitoConta = "X",
            CarteiraPadrao = "19",
            TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
            TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
            TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
        };
        _banco = Banco.Instancia(Bancos.Bradesco);
        _banco.Beneficiario = TestUtils.GerarBeneficiario("1411213", "", "", contaBancaria);
        _banco.FormataBeneficiario();
    }

    [Test]
    public void Bradesco_19_REM240()
    {
        TestUtils.TestarHomologacao(_banco, TipoArquivo.CNAB240, nameof(BancoBradescoCarteira19), 5, true, "?", 223344);
    }

    [Test]
    public void Bradesco_19_REM400()
    {
        TestUtils.TestarHomologacao(_banco, TipoArquivo.CNAB400, nameof(BancoBradescoCarteira19), 5, true, "?", 223344);
    }

    [TestCase(141.50, "453", "BB943A", "1", "019/00000000453-4", "23791690400000141501234190000000045301234560", "23791.23413 90000.000043 53012.345608 4 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "2", "019/00000000456-P", "23792693400002717161234190000000045601234560", "23791.23413 90000.000043 56012.345601 7 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "3", "019/00000000444-8", "23793690500000297211234190000000044401234560", "23791.23413 90000.000043 44012.345607 8 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "4", "019/00000000468-0", "23794693500000297211234190000000046801234560", "23791.23413 90000.000043 68012.345606 9 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "5", "019/00000000443-2", "23795690500000297211234190000000044301234560", "23791.23413 90000.000043 43012.345609 1 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "6", "019/00000000414-9", "23796687300000649391234190000000041401234560", "23791.23413 90000.000043 14012.345600 1 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "7", "019/00000000561-7", "23797702600000270001234190000000056101234560", "23791.23413 90000.000050 61012.345601 1 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "8", "019/00000000445-9", "23798690500002924111234190000000044501234560", "23791.23413 90000.000043 45012.345604 2 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "9", "019/00000000562-5", "23799702600000830001234190000000056201234560", "23791.23413 90000.000050 62012.345609 3 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_19_com_linha_digitavel_valida(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
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

    [TestCase(141.50, "453", "BB943A", "4", "019/00000000453-4", "23791690400000141501234190000000045301234560", "23791.23413 90000.000043 53012.345608 4 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "7", "019/00000000456-P", "23792693400002717161234190000000045601234560", "23791.23413 90000.000043 56012.345601 7 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "8", "019/00000000444-8", "23793690500000297211234190000000044401234560", "23791.23413 90000.000043 44012.345607 8 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "9", "019/00000000468-0", "23794693500000297211234190000000046801234560", "23791.23413 90000.000043 68012.345606 9 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "1", "019/00000000443-2", "23795690500000297211234190000000044301234560", "23791.23413 90000.000043 43012.345609 1 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "1", "019/00000000414-9", "23796687300000649391234190000000041401234560", "23791.23413 90000.000043 14012.345600 1 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "1", "019/00000000561-7", "23797702600000270001234190000000056101234560", "23791.23413 90000.000050 61012.345601 1 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "2", "019/00000000445-9", "23798690500002924111234190000000044501234560", "23791.23413 90000.000043 45012.345604 2 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "3", "019/00000000562-5", "23799702600000830001234190000000056201234560", "23791.23413 90000.000050 62012.345609 3 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_19_com_digito_verificador_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
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

    [TestCase(141.50, "453", "BB943A", "4", "019/00000000453-8", "23791690400000141501234190000000045301234560", "23791.23413 90000.000043 53012.345608 4 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "7", "019/00000000456-2", "23792693400002717161234190000000045601234560", "23791.23413 90000.000043 56012.345601 7 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "8", "019/00000000444-9", "23793690500000297211234190000000044401234560", "23791.23413 90000.000043 44012.345607 8 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "9", "019/00000000468-6", "23794693500000297211234190000000046801234560", "23791.23413 90000.000043 68012.345606 9 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "1", "019/00000000443-0", "23795690500000297211234190000000044301234560", "23791.23413 90000.000043 43012.345609 1 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "1", "019/00000000414-7", "23796687300000649391234190000000041401234560", "23791.23413 90000.000043 14012.345600 1 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "1", "019/00000000561-5", "23797702600000270001234190000000056101234560", "23791.23413 90000.000050 61012.345601 1 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "2", "019/00000000445-7", "23798690500002924111234190000000044501234560", "23791.23413 90000.000043 45012.345604 2 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "3", "019/00000000562-3", "23799702600000830001234190000000056201234560", "23791.23413 90000.000050 62012.345609 3 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_19_com_nosso_numero_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
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


    [TestCase(141.50, "453", "BB943A", "4", "019/00000000453-8", "23794690400000141501234190000000045301234560", "23791.23413 90000.000043 53012.345608 4 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "7", "019/00000000456-2", "23797693400002717161234190000000045601234560", "23791.23413 90000.000043 56012.345601 7 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "8", "019/00000000444-9", "23798690500000297211234190000000044401234560", "23791.23413 90000.000043 44012.345607 8 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "9", "019/00000000468-6", "23799693500000297211234190000000046801234560", "23791.23413 90000.000043 68012.345606 9 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "1", "019/00000000443-0", "23791690500000297211234190000000044301234560", "23791.23413 90000.000043 43012.345609 1 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "1", "019/00000000414-7", "23791687300000649391234190000000041401234560", "23791.23413 90000.000043 14012.345600 1 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "1", "019/00000000561-5", "23791702600000270001234190000000056101234560", "23791.23413 90000.000050 61012.345601 1 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "2", "019/00000000445-7", "23792690500002924111234190000000044501234560", "23791.23413 90000.000043 45012.345604 2 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "3", "019/00000000562-3", "23793702600000830001234190000000056201234560", "23791.23413 90000.000050 62012.345609 3 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_19_com_codigo_de_barras_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
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