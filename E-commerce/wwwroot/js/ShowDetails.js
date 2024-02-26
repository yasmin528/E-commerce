// Get the quantity input and plus/minus buttons
const quantityInput = document.getElementById('quantity');
const btnPlus = document.querySelector('.btn-plus');
const btnMinus = document.querySelector('.btn-minus');
const AddToCart = document.querySelector('.add-to-cart-button');
const customDialog = document.getElementById('custom-dialog');
const customConfirmButton = document.getElementById('custom-confirm');
const customCancelButton = document.getElementById('custom-cancel');
const ProductId = document.querySelector('.productId');

// Add event listeners for the plus and minus buttons
btnPlus.addEventListener('click', () => {
    // Increment the input value by 1, if it's within the allowed range
    if (parseInt(quantityInput.value) <= parseInt(quantityInput.getAttribute('max'))) {
        quantityInput.value = parseInt(quantityInput.value) + 1;
    }
});

btnMinus.addEventListener('click', () => {
    // Decrement the input value by 1, if it's greater than the minimum value
    if (parseInt(quantityInput.value) >= parseInt(quantityInput.getAttribute('min'))) {
        quantityInput.value = parseInt(quantityInput.value) - 1;
    }
});


function Add() {
    if (parseInt(quantityInput.value) >= parseInt(quantityInput.getAttribute('min')) && parseInt(quantityInput.value) <= parseInt(quantityInput.getAttribute('max')+1)) {
        
        if (AddToCart.classList.contains('user')) {
            alert("Added to cart");
            window.location.href = `/Customer/AddToCart?quantity=${parseInt(quantityInput.value)}&PId=${parseInt(ProductId.value)}`;
        } else {
            // Show the custom dialog
            customDialog.style.display = 'flex';

            // Handle the custom "Sign In" button click
            customConfirmButton.addEventListener('click', () => {
                window.location.href = "/User/Login";
            });

            // Handle the custom "Cancel" button click
            customCancelButton.addEventListener('click', () => {
                // Close the custom dialog
                customDialog.style.display = 'none';
            });
        }
    }
}
