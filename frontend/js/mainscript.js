var current_user;

$( document ).ready(function() {

  getUserDetails(function(data) {
    $('#name').text(data.userName);
    current_user = data.id;
  });

  setupSignupButtons();
  setupCreateTableForm();
  setupSearch();

  fetchTables();

  fetchGames();

});

// Ustawianie buttonów do zapisywania się do stołu
var setupSignupButtons = function() {

  $(".rooms").on('click', '.btn-warning', function(e) {
    var or = $(e.currentTarget);
    var parent = $(or).parents('.boxy')[0];
    var gr = $(parent).find('.btn-success');

    var data = { "tableId": $(parent).data("tableid") };

    authorizedPost("api/table/join", data, function(){
      getUserDetails(function(data) {
        var imurl = "../files/images/" + data.image;
        var empty = $(parent).find(".empty-user").first()[0];
        $(empty).css({"background-image": "url("+imurl+")",
                      "opacity": ".4"});
        $(empty).attr("data-userid", data.id);
        });
    });

    or.addClass("hidden");
    gr.removeClass("hidden");
  });

  $(".rooms").on('click', '.btn-success', function(e) {
    var gr = $(e.currentTarget);
    var parent = $(gr).parents('.boxy')[0];
    var or = $(parent).find('.btn-warning');

    var tableid = $(parent).data("tableid");
    tableid = "api/table/leave/" + tableid;

    authorizedPost(tableid, null, function(){
      getUserDetails(function(data) {
        var userid = data.id;
        $(parent).find("[data-userid='" + userid + "']").remove();
        $(parent).find(".users").append('<div class="empty-user"></div>');
        });
    });

    or.removeClass("hidden");
    gr.addClass("hidden");
  });

}

// Pobieranie wszystkich stołów
var fetchTables = function(city, date, game) {

  $.getJSON(serviceBase + 'api/table/all', function(data) {

    for (i = 0; i < data.length; i++) {

      if (date && Math.abs(Date.parse(date) - Date.parse(data[i].eventDate)) > 86400000 ) continue;
      if (city && city != data[i].city) continue;
      if (game && game != data[i].game.id) continue;

      var html = "";
      html += "<div class='boxy' data-tableid='" + data[i].id + "'>";
      html += '<div class="game-content"><a href="tables.html?id='+ data[i].id +'"><img src="../files/images/' + data[i].game.image + '"/></a>';
      html += '<div class="text-content"><div class="btn btn-warning btn-fab"><i class="fa fa-user-plus"></i></div><div class="btn btn-success btn-fab"><i class="fa fa-check"></i></div><h4>' + data[i].game.name + '</h4><table>';
      html += '<tr><td class="lead-cl">Miejsce: </td><td>' + data[i].localizationName + '</td></tr>';
      html += '<tr><td class="lead-cl">Termin: </td><td>' + data[i].eventDate + '</td></tr>';
      html += '<tr><td class="lead-cl">Poziom gry: </td><td>' + data[i].difficulty + '</td></tr>';
      html += '<tr><td class="lead-cl">Poziom agresji: </td><td>' + data[i].aggresionLevel + '</td></tr></table>';
      html += '<div class="users">';

      for (j = 0; j < data[i].participants.length; j++) {
        if(data[i].participants[j].status == "Pending") {
          html+= '<div class="user" data-userid="' + data[i].participants[j].id +'"><img src="../files/images/' + data[i].participants[j].image + '" class="usrimg" style="opacity: .4" / ></div>';
        } else {
          html+= '<div class="user" data-userid="' + data[i].participants[j].id +'"><img src="../files/images/' + data[i].participants[j].image + '" class="usrimg" / ></div>';
        }
      }

      if (data[i].participants.length < data[i].usersRequired) {
        for (k = 0; k < data[i].usersRequired - data[i].participants.length; k++) {
          html += '<div class="empty-user"></div>';
        }
      }

      html += '</div></div></div></div>';

      var joined = false;
      for (j = 0; j < data[i].participants.length; j++) {
        if (data[i].participants[j].id == current_user) joined = true;
      }

      html = $(html);

      if (joined) {
        var warning = html.find(".btn-warning")[0];
        $(warning).addClass("hidden");
      } else {
        var success = html.find(".btn-success")[0];
        $(success).addClass("hidden");
      }

      $(".rooms").append(html);
    }

  })
}

var fetchGames = function() {

  $.getJSON(serviceBase + 'api/game/all', function(data) {
    $.each(data, function(index, value) {
      $(".game")
          .append($("<option></option>")
          .attr("value", value.id)
          .text(value.name)); 
    });
  });

}

var setupCreateTableForm = function() {
  $("#create-table").on('submit', function() {

    var formData = {
      description: $("#tableName").val(),
      gameId: $("#game").val(),
      city: $("#city").val(),
      localizationName: $("#place").val(),
      eventDate: $("#gamedate").val() + " " + $("#gameTime").val(),
      difficulty: $("#difficulty-level").val(),
      aggresionLevel: $('input[name=aggro]:checked', '#create-table').val(),
      usersRequired: $("#usersCount").val()
    };

    authorizedPost('api/table/create', formData, function(data) {
      window.location = "tables.html?id=" + data.id
    });

    return false;
  });
}

var setupSearch = function() {
  $("#search").on('click', function() {
    var date = $("#search-date").val();
    var city = $("#search-city").val();
    var game = $("#search-game").val();

    if (date == "") {
      date = undefined;
    }

    if (city == "") {
      city = undefined;
    }

    if (game == "all") {
      game = undefined;
    }

    $(".rooms").empty();

    fetchTables(city, date, game);
  });
}