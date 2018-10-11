let aimlJs = (function ($) {   


    ///Methods
    let init = () => {

    }
    let SendMessage = (msg, sendAgain) => {
        if (!sendAgain) {
            DisplayMessage(msg, "Me");
            $("#txtMessage").val('');
            $("#txtMessage").focus();
            $('#chartAreaLog').scrollTop(1E10)
        }
        DisplayMessage("Thinking.....", "Bot");


        var url = "/Bot/Post"
        $.ajax({
            type: "POST",
            url: url,
            data: { msg: msg },
            complete: function (result) {                
                if (!CheckAnswer(msg, result.responseText) && !sendAgain) {
                    SearchInWeb(msg);
                } else
                    DisplayMessage(result.responseText, "bot");
            }
        });
    }
    let CheckAnswer = (requestMessage, responseMessage) => {       
        if ((responseMessage == "" || responseMessage.trim() == '"" .'))
            return false;        
        else
            return true;
    }
    let SearchInWeb = (msg) => {
        DisplayMessage("I don't know that yet! i am searching in web ...", "bot");
        var normalizeText = NormalizeTextToSearchInWeb(msg);
        SendMessage("searchandlearn " + normalizeText, true);
    }
    let NormalizeTextToSearchInWeb = (msg) => {
        var excludetext = ['what ', 'where ', 'who ', ' is ', ' am ', ' are ', 'do you know '];
        msg = msg.toLowerCase();
        $.each(excludetext, function (i) {
            if (msg.indexOf(excludetext[i]) != -1)
                msg = msg.replace(excludetext[i].trim(), '');
        });
        return msg.trim();
    }
    let DisplayMessage = (message, sendBy) => {
        var msg = "";
        if (sendBy == "Me")
            msg = "<span class='msg pull-right label-success'>" + message.replace(/</g, "&lt;").replace(/>/g, "&gt;") + "</span><div class='br'></div><br /> \
                           <span class='msgDate pull-right small'> " + getDate() + " </span> \
                      <div class='clearfix'></div>";
        else if (sendBy == "Bot" && message == "Thinking.....") { }
        else
            msg = "<span class='msg pull-left label-warning'> " + message + "</span><div class='br'></div><br /> \
                       <span class='msgDate pull-left small'> " + getDate() + " </span> \
                     <div class='clearfix'></div>";
        $('#chartAreaLog').append(msg);
        $('#chartAreaLog').scrollTop(1E10)

        //thinking
        if (sendBy == "Bot" && message == "Thinking.....")
            $("#txtMessage").attr("placeholder", "Thinking ......");
        else
            $("#txtMessage").attr("placeholder", "Type message here ...");
    }
    let getDate = () => {
        const date = new Date();
        return date.toLocaleTimeString();
    }

    ///Events
    $("#SendMessage").on('click', function () {
        SendMessage($("#txtMessage").val());
    });
    $("#txtMessage").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#SendMessage").click();
        }
    });

    return {
        init : init
    }
})(jQuery);
