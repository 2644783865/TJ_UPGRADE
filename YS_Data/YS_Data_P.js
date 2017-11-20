
//验证输入的是数字，且最多两位小数  
function validateNumber(e) {
    var ele = e.target;
    var reg = /^[0-9]+(.[0-9]{1,2})?$/;
    if (!reg.test($.trim($(ele).val()))) {
        e.focus();
    }
    
}

function validateNotNull(e) {
}