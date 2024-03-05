using Microsoft.EntityFrameworkCore;

namespace DemoProject.Common
{
    public class PaginatedList<T> : List<T>, IPaging
    {
        public const int PAGE_SIZE = 20;
        // 表示するページのページ番号
        public int PageIndex { get; set; }

        // ページ総数
        public int TotalPages { get; set; }

        // 1 ページに表示するレコードの数
        public int PageSize { get; set; }

        public string Url { get; set; }

        // コンストラクタ。下の CreateAsync メソッドから呼ばれる
        private PaginatedList(List<T> items,
                                int count,
                                int pageIndex,
                                int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        // 引数としてPagenatedListを受け渡す際に必要なデフォルトコンストラクタ
        public PaginatedList() { }

        // 表示するページの前にページがあるか？
        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }

        // 表示するページの後にページがあるか？
        public bool HasNextPage
        {
            get
            {
                return PageIndex < TotalPages;
            }
        }
        // 下の静的メソッドがコントローラーから呼ばれて戻り値がモデルとしてビューに渡される。
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).
                                        Take(pageSize).
                                        ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).
                                        Take(pageSize).
                                        ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }

    public interface IPaging
    {
        public const int NumPageDisplayed = 10;

        // 表示するページのページ番号
        public int PageIndex { get; set; }

        // ページ総数
        public int TotalPages { get; set; }

        // 1ページに表示するレコードの数
        public int PageSize { get; set; }

        public bool HasPreviousPage { get; }

        // 表示するページの後にページがあるか
        public bool HasNextPage { get; }

        public string Url { get; set; }
    }
}
