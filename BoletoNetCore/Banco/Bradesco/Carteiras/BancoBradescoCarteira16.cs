using System;

namespace BoletoNetCore
{
    [CarteiraCodigo("016")]
    internal class BancoBradescoCarteira16 : BancoBradescoCarteiraBase, ICarteira<BancoBradesco>
    {
        internal static Lazy<ICarteira<BancoBradesco>> Instance { get; } = new Lazy<ICarteira<BancoBradesco>>(() => new BancoBradescoCarteira16());

        private BancoBradescoCarteira16()
        {
        }
    }
}
