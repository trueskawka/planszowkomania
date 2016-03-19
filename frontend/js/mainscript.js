$( document ).ready(function() {

  getUserDetails(function(data) {
    $('#name').text(data.userName);
  });



    $(".boxy").each(function(){
      var or = $(this).find(".btn-warning");
      var gr = $(this).find(".btn-success");

      or.on("click", function(){
        or.addClass("hidden");
        gr.removeClass("hidden");
      });

      gr.on("click", function(){
        or.removeClass("hidden");
        gr.addClass("hidden");
      });

    });

    /*$.ajax({
      beforeSend: function (xhr) {
        xhr.setRequestHeader ("Authorization", "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJuYW1laWQiOiJjOTdkYmVjNy1iZWUyLTQzYmItOGYwNy1hNWY0MjdkNWEyOWYiLCJ1bmlxdWVfbmFtZSI6ImphYmJlcndpY2tlZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vYWNjZXNzY29udHJvbHNlcnZpY2UvMjAxMC8wNy9jbGFpbXMvaWRlbnRpdHlwcm92aWRlciI6IkFTUC5ORVQgSWRlbnRpdHkiLCJBc3BOZXQuSWRlbnRpdHkuU2VjdXJpdHlTdGFtcCI6ImRlMmE4MjQxLWZkYTctNDJiNC1hZWRjLTBhY2EyNmFkYWIxMCIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6MjE0MCIsImF1ZCI6IjQxNGUxOTI3YTM4ODRmNjhhYmM3OWY3MjgzODM3ZmQxIiwiZXhwIjoxNDYwOTUwMjY0LCJuYmYiOjE0NTgzNTgyNjR9.qQF5kM-A4T64hE_zmazGeZc2rOmKQ-fHy-ah4xSD8mQ");
      },
      data: {
        "description": "Wspaniala gra w Catana",
        "eventDate": "2016-04-01 10:00:00",
        "localizationName": "Paradox Cafe",
        "city": "Warszawa",
        "difficulty": 1,
        "aggresionLevel": 2,
        "usersRequired": 4,
        "gameId": 1,
      },
      type: "POST",
      url: "http://vpn.geeksoft.pl:2140/api/table/create",
      success:
      function(data){console.log(data)}
    })*/

    var url = "http://vpn.geeksoft.pl:2140/api/table/all";

    $.getJSON(url, function(data) {
        console.log(data[0]);

        for (i = 0; i < data.length; i++) {
          var html = "";
          html += "<div class='boxy'>";
          html += '<div class="game-content"><img src="../files/images/' + data[i].game.image + '"/>';
          html += '<div class="text-content"><div class="btn btn-warning btn-fab"><i class="fa fa-user-plus"></i></div><div class="btn btn-success btn-fab hidden"><i class="fa fa-check"></i></div><h4>' + data[i].game.name + '</h4><table>';
          html += '<tr><td class="lead-cl">Miejsce: </td><td>' + data[i].localizationName + '</td></tr>';
          html += '<tr><td class="lead-cl">Termin: </td><td>' + data[i].eventDate + '</td></tr>';
          html += '<tr><td class="lead-cl">Poziom gry: </td><td>' + data[i].difficulty + '</td></tr>';
          html += '<tr><td class="lead-cl">Poziom agresji: </td><td>' + data[i].aggresionLevel + '</td></tr></table>';
          html += '<div class="users">';

          for (j = 0; j < data[i].participants.length; j++) {
              html+= '<div class="user"><img src="../files/images/' + data[i].participants[j].image + '" class="usrimg" / ></div>';
          }

          if (data[i].participants.length < 4) {
            for (k = 0; k < 4 - data[i].participants.length; k++) {
              html += '<div class="empty-user"></div>';
            }
          }

          html += '</div></div></div></div>';

          $(".rooms").append(html);
        }

    })

    /*$.ajax({
      beforeSend: function (xhr) {
        xhr.setRequestHeader ("Authorization", "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJuYW1laWQiOiJjOTdkYmVjNy1iZWUyLTQzYmItOGYwNy1hNWY0MjdkNWEyOWYiLCJ1bmlxdWVfbmFtZSI6ImphYmJlcndpY2tlZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vYWNjZXNzY29udHJvbHNlcnZpY2UvMjAxMC8wNy9jbGFpbXMvaWRlbnRpdHlwcm92aWRlciI6IkFTUC5ORVQgSWRlbnRpdHkiLCJBc3BOZXQuSWRlbnRpdHkuU2VjdXJpdHlTdGFtcCI6ImRlMmE4MjQxLWZkYTctNDJiNC1hZWRjLTBhY2EyNmFkYWIxMCIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6MjE0MCIsImF1ZCI6IjQxNGUxOTI3YTM4ODRmNjhhYmM3OWY3MjgzODM3ZmQxIiwiZXhwIjoxNDYwOTUwMjY0LCJuYmYiOjE0NTgzNTgyNjR9.qQF5kM-A4T64hE_zmazGeZc2rOmKQ-fHy-ah4xSD8mQ");
      },
      type: "GET",
      url: "http://vpn.geeksoft.pl:2140/api/table/UserTables",
      success:
      function(data){console.log(data)}

    })*/





});

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

// Ustawianie buttonów do zapisywania się do stołu
var setupSignupButtons = function() {

}

// Pobieranie wszystkich stołów
var fetchUserTables = function() {

}