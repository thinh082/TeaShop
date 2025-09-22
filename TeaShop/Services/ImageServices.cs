namespace TeaShop.Services
{
    public class ImageServices
    {
        public async Task<string?> SaveImg(IFormFile image, string path, string? defaultName = null)
        {
            try
            {
                if (image == null || string.IsNullOrEmpty(path))
                {
                    return null;
                }
                string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                // Tạo tên file ảnh duy nhất (nếu không truyền defaultName)
                var extension = Path.GetExtension(image.FileName);
                var uniqueFileName = !string.IsNullOrEmpty(defaultName)
                    ? defaultName + extension
                    : Path.GetFileNameWithoutExtension(image.FileName) + "_" + Guid.NewGuid() + extension;

                // Đường dẫn đầy đủ để lưu ảnh
                var fullPath = Path.Combine(pathToSave, uniqueFileName);

                // Lưu file vào thư mục
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Trả về đường dẫn tương đối để lưu vào DB
                return Path.Combine(path, uniqueFileName).Replace("\\", "/");
            }
            catch
            {
                return null;
            }
        }


    }
}
