﻿using BoletoNetCore.Extensions;
using System;
using static System.String;

namespace BoletoNetCore
{
    public abstract class BancoBradescoCarteiraBase
    {
        public virtual void FormataNossoNumero(Boleto boleto)
        {
            // Nosso número não pode ter mais de 11 dígitos

            if (IsNullOrWhiteSpace(boleto.NossoNumero) || boleto.NossoNumero == "00000000000")
            {
                // Banco irá gerar Nosso Número
                boleto.NossoNumero = new String('0', 11);
                boleto.NossoNumeroDV = "0";
                boleto.NossoNumeroFormatado = "000/00000000000-0";
            }
            else
            {
                // Nosso Número informado pela empresa
                if (boleto.NossoNumero.Length > 11)
                    throw new Exception($"Nosso Número ({boleto.NossoNumero}) deve conter 11 dígitos.");
                boleto.NossoNumero = boleto.NossoNumero.PadLeft(11, '0');
                boleto.NossoNumeroDV = (boleto.Carteira + boleto.NossoNumero).CalcularDVBradesco();
                boleto.NossoNumeroFormatado = $"{boleto.Carteira.PadLeft(3, '0')}/{boleto.NossoNumero}-{boleto.NossoNumeroDV}";
            }
        }

        public virtual string FormataCodigoBarraCampoLivre(Boleto boleto)
        {
            var contaBancaria = boleto.Banco.Beneficiario.ContaBancaria;
            var carteira = boleto.Carteira.Length == 3 ? boleto.Carteira.Replace("0", Empty) : boleto.Carteira;

            return $"{contaBancaria.Agencia}{carteira}{boleto.NossoNumero}{contaBancaria.Conta}{"0"}";
        }
    }
}
