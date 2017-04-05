var isRegUserFormValid = true;

function tryToSubmitRegForm() {
	var formData = $("#Reg_Form");

	validateRegUserForm(formData);

	if (isRegUserFormValid) {
		var data = objectifyForm(formData.serializeArray());

		$.ajax({
			url: "http://localhost:64666/account/register",
			method: 'POST',
			contentType: 'application/x-www-form-urlencoded; charset=utf-8',
			data: data,
			success: function (result) {
				window.location.replace(HomePageUrl);
			},
			error: function (error) {
				if (error.status == 400) {
					validateRegUserForm();
				} else {
					alert("Вибачте сталася помилка. Адміністратор повідомлений про помилку і працює над її виправленням");
				}
			}
		});
	}
}

function validateRegUserForm(formData) {
	var firstName = $(formData).find("#FirstName").val();
	var lastName = $(formData).find("#LastName").val();
	var email = $(formData).find("#Email").val();
	var password = $(formData).find("#Password").val();
	var passwordConfirm = $(formData).find("#PasswordConfirm").val();

	var errorMessage = "";
	isRegUserFormValid = true;

	if (firstName.trim().length == 0) {
		errorMessage = "Ім\'я не може бути пустим";
		isRegUserFormValid = false;
	} 
	$(formData).find("#FirstName").parent().find(".reg-form-error").html(errorMessage);

	if (lastName.trim().length == 0) {
		errorMessage = "Прізвище не може бути пустим";
		isRegUserFormValid = false;
	}
	$(formData).find("#LastName").parent().find(".reg-form-error").html(errorMessage);

	if (!validateEmail(email)) {
		errorMessage = "Введіть коректний email. Приклад: Example@gmail.com";
		isRegUserFormValid = false;
	}
	$(formData).find("#Email").parent().find(".reg-form-error").html(errorMessage);

	if (password != passwordConfirm) {
		errorMessage = "Паролі не співпадають";
		isRegUserFormValid = false;

		$(formData).find("#Password").parent().find(".reg-form-error").html(errorMessage);
		$(formData).find("#PasswordConfirm").parent().find(".reg-form-error").html(errorMessage);
	} else if (password.length < 8) {
		errorMessage = "Пороль не повинен бути коротшим 8 символів";
		isRegUserFormValid = false;

		$(formData).find("#Password").parent().find(".reg-form-error").html(errorMessage);
		$(formData).find("#PasswordConfirm").parent().find(".reg-form-error").html(errorMessage);
	} else {
		var reLetter = /.*[a-z]+.*$/i;
		var reDigit = /.*[0-9]+$.*/i;
		if (!reLetter.test(password) || !reDigit.test(password)) {
			errorMessage = "Пароль повинен містити букви і цифри";
			isRegUserFormValid = false;

			$(formData).find("#Password").parent().find(".reg-form-error").html(errorMessage);
			$(formData).find("#PasswordConfirm").parent().find(".reg-form-error").html(errorMessage);
		}
	}

}

function validateEmail(email) {
	var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	return re.test(email);
}


function objectifyForm(formArray) {//serialize data function

	var result = formArray[0]['name'] + "=" + formArray[0]['value'];
	for (var i = 1; i < formArray.length; i++) {
		result += "&" + formArray[i]['name'] + "=" + formArray[i]['value'];
	}
	return result;
}