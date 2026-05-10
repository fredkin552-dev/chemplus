// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// --- DEMONSTRATION: Accessing Backend from Frontend ---
// You can call this function from the browser console, or bind it to a button click
async function loadProductsFromBackend() {
    try {
        console.log("Fetching data from backend...");
        
        // 1. Make the request to the new API endpoint
        const response = await fetch('/api/products');
        
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        
        // 2. Parse the JSON data sent by the C# controller
        const products = await response.json();
        
        console.log("Success! Received backend data:", products);
        
        // 3. Do something with the data (e.g., render HTML dynamically)
        // products.forEach(p => { ... create div elements ... })
        
    } catch (error) {
        console.error("Failed to fetch from backend:", error);
    }
}
