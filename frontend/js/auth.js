$( document ).ready(function() {

  $('#form-signin').on('submit', function(data) {

    var username = $('#inputUserName').val();
    var password = $('#inputPassword').val();

    var data = "grant_type=password&username=" + username + "&password=" + password;

    $.ajax({
      url: serviceBase + 'oauth/token',
      type: 'POST',
      data: data,
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      success: function(data) {
        localStorage.setItem('auth', JSON.stringify(data));
        window.location = "index.html"
      }
    });

    return false;
  });

  $('#logout').on('click', function() {
    localStorage.clear();
    window.location = "login.html"
  });

});