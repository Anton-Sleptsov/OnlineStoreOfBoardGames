$(document).ready(function () {

	$('.check-login').click(function (event) {
		$('.has-to-wait').attr('disabled', 'disabled');

		const loginInput = $('[name=Login]');
		loginInput.removeClass('available');
		loginInput.removeClass('input-validation-error');

		const userName = loginInput.val();

		const url = `/api/Auth/IsLoginAvailable?login=${userName}`;
		const promise = $.get(url);
		promise.done(function (isAvailable) {
			if (isAvailable) {
				loginInput.addClass('available');
			} else {
				loginInput.addClass('input-validation-error');
			}

			$('.has-to-wait').removeAttr('disabled', 'disabled');
		});

		event.preventDefault();
	});

	const debounceCheckLogin = debounce(checkLogin, 500);

	$('[name=Login]').on('keyup', function () {
		debounceCheckLogin.call();
	});

	function checkLogin() {
		const loginInput = $('[name=Login]');
		const userName = loginInput.val();
		const url = `/api/Auth/IsLoginAvailable?login=${userName}`;
		$.get(url)
			.done(function (isAvailable) {
				if (isAvailable) {
					loginInput.addClass('available');
				} else {
					loginInput.addClass('input-validation-error');
				}
			})
			.fail(function () {
				alert('server is bad');
			});
	}
});