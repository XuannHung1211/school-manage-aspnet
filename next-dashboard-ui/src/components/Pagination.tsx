

const Pagination = (
  {currentPage , totalPages  , onPageChange } 
  :
  {currentPage : number , totalPages : number , onPageChange : (page : number)=> void }) => {

  const maxVisible = 10 
  const startPage = Math.max(1 , currentPage - Math.floor(maxVisible/2))
  const endPage = Math.min(totalPages, startPage + maxVisible -1 ) 

  const pages: number[] = [];
  for (let i = startPage; i <= endPage; i++) {
    pages.push(i);
  }

  
  return (
    <div className="p-4 flex items-center justify-between text-gray-500">
      <button
        disabled ={currentPage === 1}
        onClick={() => onPageChange(currentPage - 1)}
        className="py-2 px-4 rounded-md bg-slate-200 text-xs font-semibold disabled:opacity-50 disabled:cursor-not-allowed"
      >
        Prev
      </button>
      <div className="flex items-center gap-2 text-sm">
        {startPage > 1 && (
          <>
            <button
              onClick={() => onPageChange(1)}
              className="px-2 rounded-sm hover:bg-slate-200"
            >
              1
            </button>
            {startPage > 2 && <span>...</span>}
          </>
        )}

        {pages.map((page) => (
          <button
            key={page}
            onClick={() => onPageChange(page)}  
            className={`px-2 rounded-sm ${
              page === currentPage
                ? "bg-blue-500 text-white font-semibold"
                : "hover:bg-slate-200"
            }`}
          >
            {page}
          </button>
        ))}

        {endPage < totalPages && (
          <>
            {endPage < totalPages - 1 && <span>...</span>}
            <button
              onClick={() => onPageChange(totalPages)} 
              className="px-2 rounded-sm hover:bg-slate-200"
            >
              {totalPages}
            </button>
          </>
        )}
      </div>
      <button 
      disabled={currentPage === endPage}
      onClick={()=> onPageChange(currentPage + 1)}
      className="py-2 px-4 rounded-md bg-slate-200 text-xs font-semibold disabled:opacity-50 disabled:cursor-not-allowed">
        Next
      </button>
    </div>
  );
};

export default Pagination;
