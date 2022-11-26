$("#content").keyup(function(){
    $("#counter").text($(this).val().length);
  });