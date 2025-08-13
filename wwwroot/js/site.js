// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function initProductsPage() {
	var searchInput = document.getElementById('searchInput');
	var productWrappers = Array.prototype.slice.call(document.querySelectorAll('.product-card-wrapper'));

	function normalize(text) {
		return (text || '').toString().toLowerCase().trim();
	}

	function filterProducts() {
		if (!searchInput) return;
		var term = normalize(searchInput.value);
		productWrappers.forEach(function (wrapper) {
			var name = normalize(wrapper.querySelector('.product-title')?.textContent);
			var desc = normalize(wrapper.querySelector('.product-description')?.textContent);
			var cat = normalize(wrapper.querySelector('.category-tag')?.textContent);
			var matches = !term || name.indexOf(term) !== -1 || desc.indexOf(term) !== -1 || cat.indexOf(term) !== -1;
			wrapper.style.display = matches ? '' : 'none';
		});
	}

	if (searchInput) {
		searchInput.addEventListener('input', filterProducts);
	}

	// Hover effects (no jQuery)
	var productCards = document.querySelectorAll('.product-card');
	productCards.forEach(function(card){
		card.addEventListener('mouseenter', function(){ card.classList.add('hovered'); });
		card.addEventListener('mouseleave', function(){ card.classList.remove('hovered'); });
	});
}

if (document.readyState === 'loading') {
	document.addEventListener('DOMContentLoaded', initProductsPage);
} else {
	initProductsPage();
}
