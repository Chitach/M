var scrollTime = 1000;

$(document).ready(function () {
	var hash = location.hash;
	if (hash != "" && hash.length > 1) {
		scrollToHash(hash);
	}

	$("a.scroll-to-post").on('click', function (event) {
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

	$(".post-remove-button").on("click", function (eventObj) {
		var removeLink = $(eventObj.currentTarget).parent().attr("delete-href");

		$(".delete-confirm-modal .btn-danger").attr("remove-link", removeLink);

		$(".delete-confirm-modal .btn-danger").on("click", function (e) {
			var removeLink = $(".delete-confirm-modal .btn-danger").attr("remove-link");

			$.ajax({
				url: removeLink,
				method: 'GET',
				success: function (result) {
					window.location.reload();
				},
				error: function (error) {
					alert("Вибачте сталася помилка. Адміністратор повідомлений про помилку і працює над її виправленням");
				}
			});
		})
	});

	return false;
});

function scrollToHash(hash) {
	$('html, body').animate({
		scrollTop: $(hash).offset().top - 80
	}, scrollTime);
}


