const Pagination = ({ totalPages, currentPage, onPageChange}) => {
    const renderPagination = () => {
        const pages = [];
        const pageRange = 2;

        if (currentPage > pageRange + 2) 
        {
            pages.push(
                <a key={1} href="#" onClick={() => onPageChange(1)}>
                    1
                </a>
            )
        }

        for (let i = Math.max(1, currentPage - pageRange); i <= Math.min(totalPages, currentPage + pageRange); i++)
        {
            pages.push(
                <a key={i} href="#" onClick={() => onPageChange(i)}>
                    {i}
                </a>
            )
        }

        if (currentPage < totalPages - pageRange - 1){
            pages.push(<span key="end-ellipsis">...</span>);
            pages.push(
                <a key={totalPages} href="#" onClick={() => onPageChange(totalPages)}>
                    {totalPages}
                </a>
            );
        }
        return pages;
    };

    return (
        <div className="product__pagination">
            {/* Nút Previous */}
            {currentPage > 1 && (
                <a href="#" onClick={() => onPageChange(currentPage - 1)}>
                    &lt; Prev
                </a>
            )}
            {/* Các nút phân trang */}
            {renderPagination()}
            {/* Nút Next */}
            {currentPage < totalPages && (
                <a href="#" onClick={() => onPageChange(currentPage + 1)}>
                    Next &gt;
                </a>
            )}
        </div>
    );
};

export default Pagination;