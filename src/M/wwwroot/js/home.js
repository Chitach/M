var scrollTime = 1000;

$(document).ready(function () {
	var hash = location.hash;
	if (hash != "" && hash.length > 1) {
		scrollToHash(hash);
	}

	$("a").on('click', function (event) {
		var hash = this.hash;
		scrollToHash(hash);
		if (history.pushState) {
			history.pushState(null, null, hash);
		}
		else {
			location.hash = hash;
		}

		return false;
	});

	return false;
});

function scrollToHash(hash) {
	$('html, body').animate({
		scrollTop: $(hash).offset().top
	}, scrollTime);
}
