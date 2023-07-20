// WalletPass.passType


using System.Collections.Generic;

namespace WalletPass
{
  public class passType
  {
    public List<passField> headerFields { get; set; }

    public List<passField> primaryFields { get; set; }

    public List<passField> secondaryFields { get; set; }

    public List<passField> backFields { get; set; }

    public List<passField> auxiliaryFields { get; set; }

    public string transitType { get; set; }
  }
}
