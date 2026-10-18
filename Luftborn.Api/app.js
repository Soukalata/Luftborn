
const apiUrl = "http://localhost:5162/api/products";

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

        products.forEach(product => {
            const item = document.createElement("li");
            item.textContent = `${product.name} - ${product.price}`;
            productList.appendChild(item);
        });

        status.textContent = `${products.length} product(s) loaded.`;
    } catch (error) {
        console.error("Failed to load products:", error);
        status.textContent = "Could not load products. Check the console.";
    }
}

loadProducts();
