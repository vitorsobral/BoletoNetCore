using System;

namespace BoletoNetCore
{
    [CarteiraCodigo("03")]
    public class BancoBradescoCarteira03 : BancoBradescoCarteiraBase, ICarteira<BancoBradesco>
    {
        internal static Lazy<ICarteira<BancoBradesco>> Instance { get; } = new Lazy<ICarteira<BancoBradesco>>(() => new BancoBradescoCarteira03());

        private BancoBradescoCarteira03()
        {
        }
    }
}
