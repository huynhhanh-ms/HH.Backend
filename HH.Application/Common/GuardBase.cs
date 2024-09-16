namespace PI.Application.Common
{
    public class GuardBase<T> where T : class
    {
        public delegate Task<(bool, string)> Policy(T guard);

        public List<Policy> Policies { get; private set; } = new List<Policy>();

        public async Task<(bool, string)> Screening(T? entity)
        {
            if (entity == null)
                return (false, $"{typeof(T).Name} is null");

            foreach (var policy in Policies)
            {
                var res = await policy(entity);
                if (!res.Item1)
                    return res;
            }

            return (true, string.Empty);
        }
    }
}
