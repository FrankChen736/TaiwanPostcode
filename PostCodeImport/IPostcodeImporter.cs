using System.Threading.Tasks;

namespace Postcode
{
    public interface IPostcodeImporter
    {
        Task ImportAsync(string filePath);
    }
}