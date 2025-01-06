namespace maratonAPI.Models
{
    public class Dtos
    {

        public record CreateEredmenyDto(int futo, int kor, int ido);
        public record UpdateEredmenyDto(int kor, int ido);

        public record CreateFutoDto(int id, string Fnev, int Szulev, int szulho, int csapat, bool ffi);
        public record UpdateFutoDto(int csapat, bool ffi);
    }
}
