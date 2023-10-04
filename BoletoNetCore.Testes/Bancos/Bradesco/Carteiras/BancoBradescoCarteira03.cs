using System;
using NUnit.Framework;

namespace BoletoNetCore.Testes;

[TestFixture]
[System.ComponentModel.Category("Bradesco Carteira 03")]
public class BancoBradescoCarteira03
{
    readonly IBanco _banco;
    public BancoBradescoCarteira03()
    {
        var contaBancaria = new ContaBancaria
        {
            Agencia = "1234",
            DigitoAgencia = "X",
            Conta = "123456",
            DigitoConta = "X",
            CarteiraPadrao = "03",
            TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
            TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
            TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
        };
        _banco = Banco.Instancia(Bancos.Bradesco);
        _banco.Beneficiario = TestUtils.GerarBeneficiario("1411213", "", "", contaBancaria);
        _banco.FormataBeneficiario();
    }

    [Test]
    public void Bradesco_03_REM240()
    {
        TestUtils.TestarHomologacao(_banco, TipoArquivo.CNAB240, nameof(BancoBradescoCarteira03), 5, true, "?", 223344);
    }

    [Test]
    public void Bradesco_03_REM400()
    {
        TestUtils.TestarHomologacao(_banco, TipoArquivo.CNAB400, nameof(BancoBradescoCarteira03), 5, true, "?", 223344);
    }

    [TestCase(141.50, "453", "BB943A", "1", "003/00000000453-4", "23791690400000141501234090000000045301234560", "23791.23405 30000.000049 53012.345608 7 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "2", "003/00000000456-P", "23792693400002717161234090000000045601234560", "23791.23405 30000.000049 56012.345601 1 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "3", "003/00000000444-8", "23793690500000297211234090000000044401234560", "23791.23405 30000.000049 44012.345607 1 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "4", "003/00000000468-0", "23794693500000297211234090000000046801234560", "23791.23405 30000.000049 68012.345606 1 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "5", "003/00000000443-2", "23795690500000297211234090000000044301234560", "23791.23405 30000.000049 43012.345609 2 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "6", "003/00000000414-9", "23796687300000649391234090000000041401234560", "23791.23405 30000.000049 14012.345600 3 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "7", "003/00000000561-7", "23797702600000270001234090000000056101234560", "23791.23405 30000.000056 61012.345601 4 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "8", "003/00000000445-9", "23798690500002924111234090000000044501234560", "23791.23405 30000.000049 45012.345604 5 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "9", "003/00000000562-5", "23799702600000830001234090000000056201234560", "23791.23405 30000.000056 62012.345609 6 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_03_com_linha_digitavel_valida(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
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

    [TestCase(141.50, "453", "BB943A", "7", "003/00000000453-4", "23791690400000141501234090000000045301234560", "23791.23405 30000.000049 53012.345608 7 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "1", "003/00000000456-P", "23792693400002717161234090000000045601234560", "23791.23405 30000.000049 56012.345601 1 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "1", "003/00000000444-8", "23793690500000297211234090000000044401234560", "23791.23405 30000.000049 44012.345607 1 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "1", "003/00000000468-0", "23794693500000297211234090000000046801234560", "23791.23405 30000.000049 68012.345606 1 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "2", "003/00000000443-2", "23795690500000297211234090000000044301234560", "23791.23405 30000.000049 43012.345609 2 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "3", "003/00000000414-9", "23796687300000649391234090000000041401234560", "23791.23405 30000.000049 14012.345600 3 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "4", "003/00000000561-7", "23797702600000270001234090000000056101234560", "23791.23405 30000.000056 61012.345601 4 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "5", "003/00000000445-9", "23798690500002924111234090000000044501234560", "23791.23405 30000.000049 45012.345604 5 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "6", "003/00000000562-5", "23799702600000830001234090000000056201234560", "23791.23405 30000.000056 62012.345609 6 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_09_com_digito_verificador_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
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

    [TestCase(141.50, "453", "BB943A", "1", "003/00000000453-8", "23791690400000141501234090000000045301234560", "23791.23405 30000.000043 53012.345608 1 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "2", "003/00000000456-2", "23792693400002717161234090000000045601234560", "23791.23405 30000.000043 56012.345601 2 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "3", "003/00000000444-9", "23793690500000297211234090000000044401234560", "23791.23405 30000.000043 44012.345607 3 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "4", "003/00000000468-6", "23794693500000297211234090000000046801234560", "23791.23405 30000.000043 68012.345606 4 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "5", "003/00000000443-0", "23795690500000297211234090000000044301234560", "23791.23405 30000.000043 43012.345609 5 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "6", "003/00000000414-7", "23796687300000649391234090000000041401234560", "23791.23405 30000.000043 14012.345600 6 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "7", "003/00000000561-5", "23797702600000270001234090000000056101234560", "23791.23405 30000.000050 61012.345601 7 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "8", "003/00000000445-7", "23798690500002924111234090000000044501234560", "23791.23405 30000.000043 45012.345604 8 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "9", "003/00000000562-3", "23799702600000830001234090000000056201234560", "23791.23405 30000.000050 62012.345609 9 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_09_com_nosso_numero_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
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


    [TestCase(141.50, "453", "BB943A", "1", "003/00000000453-P", "23797690400000141501234030000000045301234560", "23791.23405 30000.000043 53012.345608 1 69040000014150", 2016, 9, 1)]
    [TestCase(2717.16, "456", "BB874A", "2", "003/00000000456-4", "23791693400002717161234030000000045601234560", "23791.23405 30000.000043 56012.345601 2 69340000271716", 2016, 10, 1)]
    [TestCase(297.21, "444", "BB834A", "3", "003/00000000444-0", "23791690500000297211234030000000044401234560", "23791.23405 30000.000043 44012.345607 3 69050000029721", 2016, 9, 2)]
    [TestCase(297.21, "468", "BB856A", "4", "003/00000000468-8", "23791693500000297211234030000000046801234560", "23791.23405 30000.000043 68012.345606 4 69350000029721", 2016, 10, 2)]
    [TestCase(297.21, "443", "BB833A", "5", "003/00000000443-2", "23792690500000297211234030000000044301234560", "23791.23405 30000.000043 43012.345609 5 69050000029721", 2016, 9, 2)]
    [TestCase(649.39, "414", "BB815A", "6", "003/00000000414-9", "23793687300000649391234030000000041401234560", "23791.23405 30000.000043 14012.345600 6 68730000064939", 2016, 8, 1)]
    [TestCase(270, "561", "BB932A", "7", "003/00000000561-7", "23794702600000270001234030000000056101234560", "23791.23405 30000.000050 61012.345601 7 70260000027000", 2017, 1, 1)]
    [TestCase(2924.11, "445", "BB874A", "8", "003/00000000445-9", "23795690500002924111234030000000044501234560", "23791.23405 30000.000043 45012.345604 8 69050000292411", 2016, 9, 2)]
    [TestCase(830, "562", "BB933A", "9", "003/00000000562-5", "23796702600000830001234030000000056201234560", "23791.23405 30000.000050 62012.345609 9 70260000083000", 2017, 1, 1)]
    public void Deve_criar_boleto_bradesco_09_com_codigo_de_barras_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
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