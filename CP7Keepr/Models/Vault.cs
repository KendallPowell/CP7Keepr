namespace CP7Keepr.Models;

public class Vault
{
  public int Id { get; set; }
  public string CreatorId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public bool IsPrivate { get; set; }
  public Account Creator { get; set; }
}

public class VaultKeeps : Vault
{
  public int VaultKeepId { get; set; }
}

public class MyVaults : Vault
{
  public int VaultMemberId { get; set; }
}
