namespace CP7Keepr.Interfaces;

public interface IRepository<T, Tid>
{
  List<T> Get();
  T GetOne(Tid id);
  T Create(T body);
  bool Update(T update);
  bool Delete(Tid id);
}
