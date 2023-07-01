using Auto_Blog.Domain.Enum;

namespace Auto_Blog.Domain.Responce
{
    public interface IBaseResponse<T>
    {
        public T Data { get; set; }
        ErrorStatus Status { get; set; }
        public string Description { get; set; }
    }

    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }

        public ErrorStatus Status { get; set; }

        public T Data { get; set; }
    }
}
