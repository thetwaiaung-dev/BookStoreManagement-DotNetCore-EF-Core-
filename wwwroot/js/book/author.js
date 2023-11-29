
function createPaginationForAuthors(pages, page, pageSize, searchValue, categoryId) {

    createAllAuthor(searchValue, page, pageSize, categoryId).then(totalPages => {
        pages = totalPages;

        let str = '<ul class="custom-list">';
        let active;
        let pageCutLow = page - 1;
        let pageCutHigh = page + 1;
        // Show the Previous button only if you are on a page other than the first
        if (page > 1) {
            str += '<li class="page-item previous no"><a onclick="createPaginationForAuthors(' + pages + ', ' + (page - 1) + ',' + pageSize + ',' + '\'' + searchValue + '\'' + ',' + categoryId + ')">Previous</a></li>';
        }
        // Show all the pagination elements if there are less than 6 pages total
        if (pages < 6) {
            for (let p = 1; p <= pages; p++) {
                active = page == p ? "active" : "no";
                str += '<li class="' + active + '"><a onclick="createPaginationForAuthors(' + pages + ', ' + p + ',' + pageSize + ',' + '\'' + searchValue + '\'' + ',' + categoryId + ')">' + p + '</a></li>';
            }
        }
        // Use "..." to collapse pages outside of a certain range
        else {
            // Show the very first page followed by a "..." at the beginning of the
            // pagination section (after the Previous button)
            if (page > 2) {
                str += '<li class="no page-item"><a onclick="createPaginationForAuthors(' + pages + ', 1,' + pageSize + ',' + '\'' + searchValue + '\'' + ',' + categoryId + ')">1</a></li>';
                if (page > 3) {
                    str += '<li class="out-of-range"><a onclick="createPaginationForAuthors(' + pages + ',' + (page - 2) + ',' + pageSize + ',' + '\'' + searchValue + '\'' + ',' + categoryId + ')">...</a></li>';
                }
            }
            // Determine how many pages to show after the current page index
            if (page === 1) {
                pageCutHigh += 2;
            } else if (page === 2) {
                pageCutHigh += 1;
            }
            // Determine how many pages to show before the current page index
            if (page === pages) {
                pageCutLow -= 2;
            } else if (page === pages - 1) {
                pageCutLow -= 1;
            }
            // Output the indexes for pages that fall inside the range of pageCutLow
            // and pageCutHigh
            for (let p = pageCutLow; p <= pageCutHigh; p++) {
                if (p === 0) {
                    p += 1;
                }
                if (p > pages) {
                    continue
                }
                active = page == p ? "active" : "no";
                str += '<li class="page-item ' + active + '"><a onclick="createPaginationForAuthors(' + pages + ', ' + p + ',' + pageSize + ',' + '\'' + searchValue + '\'' + ',' + categoryId + ')">' + p + '</a></li>';
            }
            // Show the very last page preceded by a "..." at the end of the pagination
            // section (before the Next button)
            if (page < pages - 1) {
                if (page < pages - 2) {
                    str += '<li class="out-of-range"><a onclick="createPaginationForAuthors(' + pages + ',' + (page + 2) + ',' + pageSize + ',' + '\'' + searchValue + '\'' + ',' + categoryId + ')">...</a></li>';
                }
                str += '<li class="page-item no"><a onclick="createPaginationForAuthors(' + pages + ', ' + pages + ',' + pageSize + ',' + '\'' + searchValue + '\'' + ',' + categoryId + ')">' + pages + '</a></li>';
            }
        }
        // Show the Next button only if you are on a page other than the last
        if (page < pages) {
            str += '<li class="page-item next no"><a onclick="createPaginationForAuthors(' + pages + ', ' + (page + 1) + ',' + pageSize + ',' + '\'' + searchValue + '\'' + ',' + categoryId + ')">Next</a></li>';
        }
        str += '</ul>';
        // Return the pagination string to be outputted in the pug templates
        document.getElementById('pagination').innerHTML = str;
        return str;
    }).catch(error => console.log("createAllAuthor function error : " + error))
}

function createAuthor(photo, authorName,authorPhoto) {
    var author = document.createElement('div');
    author.className = "col-sm-4 p-0";
    author.innerHTML = `<a class="nav-link h-100" href="#">
                                    <div class="card h-100">
                                        <div>
                                            <img src="${authorPhoto}" style="width:275px;height:250px" />
                                        </div>
                                        <div class="card-footer w-100 h-25">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <small class="d-flex text-muted">Author</small>
                                                </div>
                                                <div class="col-sm-8 justify-content-end d-flex ">
                                                    <small class=" text-muted d-flex">${authorName}</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>`;
    allAuthor.appendChild(author);
}

 async function getAllAuthor(searchValue, pageNo, pageSize, categoryId) {
    const data = {
        searchValue: searchValue,
        pageNo: pageNo,
        pageSize: pageSize,
        categoryId: categoryId
    };

    const settings = {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
     };

    const response =await fetch('/api/bookApi/get-all-authors', settings);
    if (!response.ok) {
        const errorMessage = `An error occur :${response.status}`;
        throw new Error(errorMessage);
    }
    const authorList = response.json();
    return authorList;
}

async function createAllAuthor(searchValue, pageNo, pageRow, categoryId) {
    const authorList = await getAllAuthor(searchValue, pageNo, pageRow, categoryId);
    allAuthor.innerHTML = '';

    for await (const author of authorList.authors) {
        createAuthor('',author.author_Name,author.author_Photo)
    }
    return authorList.totalPages;
}