var serviceBase = "http://vpn.geeksoft.pl:2140/";

// Podmianka za $.getJSON tyle że z access_tokenem
var authorizedGet = function(apiPath, callback) {
  var token = getAccessToken();
  $.ajax({
    url: serviceBase + apiPath,
    type: 'GET',
    beforeSend: function (xhr) {
      xhr.setRequestHeader ("Authorization", "Bearer " + token);
    },
    success: callback
  });
}

// Postowanie z autoryzacją
var authorizedPost = function(apiPath, data, callback) {
  var token = getAccessToken();
  $.ajax({
    url: serviceBase + apiPath,
    type: 'POST',
    data: data,
    beforeSend: function (xhr) {
      xhr.setRequestHeader ("Authorization", "Bearer " + token);
    },
    success: callback
  });
}

// Pobieranie access token z localStorage
var getAccessToken = function() {
  var auth = localStorage.getItem('auth');
  if (auth) {
    auth = JSON.parse(auth);
    return auth.access_token;
  } else {
    window.location = "login.html";
  }
};

// Pobieranie detali zalogowanego użytkownika
var getUserDetails = function(callback) {
  authorizedGet('api/user/details', callback);
};
