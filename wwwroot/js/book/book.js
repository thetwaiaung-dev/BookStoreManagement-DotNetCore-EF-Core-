
function createBook(photo, title, author) {
    var book = document.createElement('div');
    book.className = "col-sm-4 p-0";
    book.innerHTML = `<a class="nav-link h-100" href="#">
                                    <div class="card h-100">
                                        <div>
                                            <img src="/photos/indexPhoto.jpg" class="w-100 h-100" />
                                        </div>
                                        <div class="card-body">
                                            <h5 class="card-title">${title}</h5>
                                            <p class="card-text"></p>
                                        </div>
                                        <div class="card-footer ">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <small class="d-flex text-muted">Author</small>
                                                </div>
                                                <div class="col-sm-8 justify-content-end d-flex ">
                                                    <small class=" text-muted d-flex">${author}</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>`;
    allBook.appendChild(book);
}

async function getAllBook(searchValue, pageNo, pageSize, categoryId, authorId) {
    Notiflix.Loading.standard('Wait...');

    const data = {
        searchValue: searchValue,
        pageNo: pageNo,
        pageSize: pageSize,
        categoryId: categoryId,
        authorId: authorId
    };

    const settings = {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    };

    const response = await fetch('/api/bookApi/get-all-books', settings);
    if (!response.ok) {
        const errorMessage = `An error occur :${response.status}`;
        throw new Error(errorMessage);
    }

    const bookList = await response.json();
    return bookList;
}

async function createAllBook(searchValue, pageNo, pageRow, categoryId, authorId) {
    const bookList = await getAllBook(searchValue, pageNo, pageRow, categoryId, authorId);
    allBook.innerHTML = '';

    setTimeout(() => {
        Notiflix.Loading.remove();
        for (const book of bookList.books) {
            createBook('', book.book_Title, book.author.author_Name)
        }
    }, 1000)
    return bookList.totalPages;
}

/* search input box for all books */
let timeoutId; // Declare a variable to store the timeout ID

if( searchValue != null) {
    searchValue.addEventListener('input', () => {
        // Clear the previous timeout if it exists
        if (timeoutId) {
            clearTimeout(timeoutId);
        }

        timeoutId = setTimeout(() => {
            createPagination(0, 1, 9, searchValue.value, 0, 0);
        }, 1000);
    });
}

function searchBookCategory(element, id) {

    if (timeoutId) {
        clearTimeout(timeoutId);
    }

    // Set a new timeout of 2000 milliseconds (2 seconds)
    timeoutId = setTimeout(() => {
        createPagination(0, 1, 9, element.value,id, 0);
    }, 1000);

}



