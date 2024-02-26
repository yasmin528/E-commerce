// Select all the plus and minus buttons within the product rows
const plusButtons = document.querySelectorAll('.plus');
const minusButtons = document.querySelectorAll('.minus');
const closeButtons = document.querySelectorAll('.close');

// Iterate through each plus button and attach the click event listener
plusButtons.forEach((button) => {
    button.addEventListener('click', () => {
        const productRow = button.closest('.row'); // Find the parent product row
        const TheProductQuantity = productRow.querySelector('.Pvalue');
        const ThequantityInput = productRow.querySelector('.quantity');
        const Tprice = productRow.querySelector('.Tprice');
        const ThepriceInput = productRow.querySelector('.P-price');
        const totalAm = document.querySelector('.totalAm');
        const totalPr = document.querySelector('.totalPr');
        let productId = document.querySelector('.p-id');
        productId = parseInt(productId.value);

        // Increment the input value by 1, if it's within the allowed range
        if (parseInt(ThequantityInput.textContent) + 1 <= parseInt(TheProductQuantity.value)) {
            ThequantityInput.textContent = parseInt(ThequantityInput.textContent) + 1;
            const newQuantity = parseInt(ThequantityInput.textContent);
            Tprice.textContent = parseFloat(Tprice.textContent) + parseFloat(ThepriceInput.value); // Update the content
            totalAm.textContent = parseInt(totalAm.textContent) + 1; // Update the content
            totalPr.textContent = parseFloat(totalPr.textContent) + parseFloat(ThepriceInput.value); // Update the content
            // Send an AJAX request to update the product quantity
            updateProductQuantity(productId, newQuantity);
        }
    });
});

// Iterate through each minus button and attach the click event listener
minusButtons.forEach((button) => {
    button.addEventListener('click', () => {
        const productRow = button.closest('.row'); // Find the parent product row
        const TheProductQuantity = productRow.querySelector('.Pvalue');
        const ThequantityInput = productRow.querySelector('.quantity');
        const Tprice = productRow.querySelector('.Tprice');
        const ThepriceInput = productRow.querySelector('.P-price');
        const totalAm = document.querySelector('.totalAm');
        const totalPr = document.querySelector('.totalPr');
        let productId = document.querySelector('.p-id');
        productId = parseInt(productId.value);
        // Decrement the input value by 1, if it's greater than the minimum value (0)
        if (parseInt(ThequantityInput.textContent) - 1 > 0) {
            ThequantityInput.textContent = parseInt(ThequantityInput.textContent) - 1;
            const newQuantity = parseInt(ThequantityInput.textContent);
            Tprice.textContent = parseFloat(Tprice.textContent) - parseFloat(ThepriceInput.value); // Update the content
            totalAm.textContent = parseInt(totalAm.textContent) - 1; // Update the content
            totalPr.textContent = parseFloat(totalPr.textContent) - parseFloat(ThepriceInput.value); // Update the content
            // Send an AJAX request to update the product quantity
            updateProductQuantity(productId, newQuantity);
        }
    });
});
closeButtons.forEach((button) => {
    button.addEventListener('click', () => {
        const productRow = button.closest('.row'); // Find the parent product row
        let productId = document.querySelector('.p-id');
        productId = parseInt(productId.value);
        
        $.ajax({
            url: '/Customer/RemoveProductQuantity', // Replace 'YourController' with your actual controller name
            method: 'POST',
            data: {
                productId: productId
            },
            success: function (response) {
                if (response.success) {
                    // Update the quantity displayed on the page
                    const productRow2 = button.closest('.cartItem');
                    productRow2.style.display = "none";
                } else {
                    // Handle error or display an error message
                    console.error('Failed to remove product quantity: ' + response.message);
                }
            },
            error: function (error) {
                // Handle error
                console.error('An error occurred while updating product quantity: ' + error);
            }
        });
    });
});
function updateProductQuantity(productId, newQuantity) {
    // Send an AJAX request to update the product quantity
    $.ajax({
        url: '/Customer/UpdateProductQuantity', // Replace 'YourController' with your actual controller name
        method: 'POST',
        data: {
            productId: productId,
            newQuantity: newQuantity
        },
        success: function (response) {
            if (response.success) {
                // Update the quantity displayed on the page
                const productRow = document.querySelector(`[data-product-id="${productId}"]`);
                const ThequantityInput = productRow.querySelector('.quantity');
                ThequantityInput.textContent = newQuantity;
            } else {
                // Handle error or display an error message
                console.error('Failed to update product quantity: ' + response.message);
            }
        },
        error: function (error) {
            // Handle error
            console.error('An error occurred while updating product quantity: ' + error);
        }
    });
}