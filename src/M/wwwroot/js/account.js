var isRegUserFormValid = true;

function tryToSubmitRegForm() {
	var form = $("#Reg_Form");
	validateRegUserForm(form);

	if (true) {
		var data = objectifyForm(form.serializeArray());

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
					validateRegUserForm(formData);
				} else {
					alert("Вибачте сталася помилка. Адміністратор повідомлений про помилку і працює над її виправленням");
				}
			}
		});
	}
}

$(document).ready(function () {
});

function tryToLogin() {
	var formData = $("#Login_Form");
	var email = $(formData).find("#Email").val();
	var password = $(formData).find("#Password").val();

	if (!validateEmail(email)) {
		$(formData).find("#Email").parent().find(".login-form-error").html("Введено не корекний email");
	} else if (password.length < 8) {
		isRegUserFormValid = false;
		$(formData).find("#Password").parent().find(".login-form-error").html("Пороль не повинен бути коротшим 8 символів");
	} else {
		var data = objectifyForm(formData.serializeArray());
		$.ajax({
			url: "http://localhost:64666/account/login",
			method: 'POST',
			contentType: 'application/x-www-form-urlencoded; charset=utf-8',
			data: data,
			success: function (result) {
				window.location.replace(HomePageUrl);
			},
			error: function (error) {
				if (error.status == 401) {
					$(formData).find("#Password").parent().find(".reg-form-error").html("Пароль невірний");
				} else {
					$(formData).find("#Email").parent().find(".reg-form-error").html("Користувача з таким email не існує");
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

	errorMessage = "";
	if (firstName.trim().length < 2) {
		errorMessage = "Надто коротке ім\'я";
		isRegUserFormValid = false;
	} 
	$(formData).find("#FirstName").parent().find(".reg-form-error").html(errorMessage);

	errorMessage = "";
	if (lastName.trim().length < 2) {
		errorMessage = "Надто коротке прізвище";
		isRegUserFormValid = false;
	}
	$(formData).find("#LastName").parent().find(".reg-form-error").html(errorMessage);

	errorMessage = "";
	if (!validateEmail(email)) {
		errorMessage = "Введіть коректний email. Приклад: Example@gmail.com";
		isRegUserFormValid = false;
	}
	$(formData).find("#Email").parent().find(".reg-form-error").html(errorMessage);

	errorMessage = "";
	if (password != passwordConfirm) {
		errorMessage = "Паролі не співпадають";
		isRegUserFormValid = false;
	} else if (password.length < 8) {
		errorMessage = "Пороль не повинен бути коротшим 8 символів";
		isRegUserFormValid = false;
	} else {
		var reLetter = /.*[a-z]+.*$/i;
		var reDigit = /.*[0-9]+$.*/i;
		if (!reLetter.test(password) || !reDigit.test(password)) {
			errorMessage = "Пароль повинен містити букви і цифри";
			isRegUserFormValid = false;
		}
	}
	$(formData).find("#Password").parent().find(".reg-form-error").html(errorMessage);
	$(formData).find("#PasswordConfirm").parent().find(".reg-form-error").html(errorMessage);

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