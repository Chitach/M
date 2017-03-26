function tryToSubmitRegForm() {
	var formData = $("#Reg_Form").serializeArray();
	formData = objectifyForm(formData);

	$.ajax({
		url: "http://localhost:64666/account/register",
		method: 'POST',
		contentType: 'application/x-www-form-urlencoded; charset=utf-8',
		data: formData,
		success: function (result) {
			
		}
	});
}

function objectifyForm(formArray) {//serialize data function

	var result = formArray[0]['name'] + "=" + formArray[0]['value'];
	for (var i = 1; i < formArray.length; i++) {
		result += "&" + formArray[i]['name'] + "=" + formArray[i]['value'];
	}
	return result;
}