$( document ).ready(function() {

  $("#reg").on("click", function(){
    $("#sigform").addClass("hidden");
    $("#regform").removeClass("hidden");
  })

  $("#log").on("click", function(){
    $("#regform").addClass("hidden");
    $("#sigform").removeClass("hidden");
  })

  var signing = function(username, password) {
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
  }

  $('#form-signin').on('submit', function(data) {

    var username = $('#inputUserName').val();
    var password = $('#inputPassword').val();

    signing(username, password);

    return false;
  });

  $('#logout').on('click', function() {
    localStorage.clear();
    window.location = "login.html"
  });


    $('#form-register').on('submit', function(data) {

      var username = $('#inputUserName-reg').val();
      var password = $('#inputPassword-reg').val();

      var data = {
        "userName": username,
        "password": password,
        "email": username +   "@vp.pl"
      }

      $.ajax({
        url: serviceBase + 'api/user/register',
        type: 'POST',
        data: data,
        success: function(data) {
          signing(username, password);
        },
        error: function(data) {
          console.log("fail");
        }
      });

      return false;
    });

});
