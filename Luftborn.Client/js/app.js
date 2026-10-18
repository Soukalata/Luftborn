
const apiUrl = "https://localhost:7242/api/products";


async function loadProducts() {
    const status = document.getElementById("status");
    const productList = document.getElementById("product-list");

    try {
        const response = await fetch(apiUrl);

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const products = await response.json();

        productList.innerHTML = "";

        status.textContent = `${products.length} product(s) loaded.`;

        // Render each product with Edit and Delete buttons.
        products.forEach(function (product) {
            const listItem = document.createElement("li");

            const productInfo = document.createElement("span");
            productInfo.textContent = `${product.name} - ${product.price}`;

            const editButton = document.createElement("button");
            editButton.textContent = "Edit";
            editButton.addEventListener("click", function () {
                editProduct(product);
            });

            const deleteButton = document.createElement("button");
            deleteButton.textContent = "Delete";
            deleteButton.addEventListener("click", function () {
                deleteProduct(product.id);
            });

            listItem.appendChild(productInfo);
            listItem.appendChild(document.createTextNode(" "));
            listItem.appendChild(editButton);
            listItem.appendChild(document.createTextNode(" "));
            listItem.appendChild(deleteButton);

            productList.appendChild(listItem);
        });
    } catch (error) {
        console.error("Failed to load products:", error);
        status.textContent = "Could not load products. Check the console.";
    }
}


/* Update an existing product using PUT. */
async function editProduct(product) {
    const name = prompt("Enter the product name:", product.name);

    // Cancelled or entered a blank name.
    if (name === null || name.trim() === "") {
        return;
    }

    const priceInput = prompt("Enter the product price:", product.price);

    if (priceInput === null || priceInput.trim() === "") {
        return;
    }

    const price = Number(priceInput);

    if (!Number.isFinite(price) || price <= 0) {
        alert("Price must be a number greater than zero.");
        return;
    }

    try {
        const response = await fetch(`${apiUrl}/${product.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                id: product.id,
                name: name.trim(),
                price: price
            })
        });

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        document.getElementById("status").textContent =
            "Product updated successfully.";

        await loadProducts();
    } catch (error) {
        console.error("Failed to update product:", error);
        document.getElementById("status").textContent =
            "Could not update product. Check the console.";
    }
}

/* Delete a product using DELETE. */
async function deleteProduct(productId) {
    const confirmed = confirm("Are you sure you want to delete this product?");

    if (!confirmed) {
        return;
    }

    try {
        const response = await fetch(`${apiUrl}/${productId}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        document.getElementById("status").textContent =
            "Product deleted successfully.";

        await loadProducts();
    } catch (error) {
        console.error("Failed to delete product:", error);
        document.getElementById("status").textContent =
            "Could not delete product. Check the console.";
    }
}


const productForm = document.getElementById("product-form");

// Handle form submission without reloading the page.
productForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    const name = document.getElementById("product-name").value.trim();
    const price = Number(document.getElementById("product-price").value);

    try {
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ name, price })
        });

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        productForm.reset();

        document.getElementById("status").textContent =
            "Product created successfully.";

        await loadProducts();
    } catch (error) {
        console.error("Failed to create product:", error);

        document.getElementById("status").textContent =
            "Could not create product. Check the console.";
    }
});

loadProducts();
