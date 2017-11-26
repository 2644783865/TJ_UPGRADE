//验证输入的数字
function validateNumber(e) {
    var reg = /^\d+(.\d{1,4})?$/
    var ele = e.target;
    if (!reg.test($.trim($(ele).val()))) {
        alert('请正确输入数字')
        $(ele).val('0.00').focus();
    }
}

function validateChar(e) {
    var reg = /[']+|[;]+/;
    var ele = e.target;
    if (reg.test($.trim($(ele).val()))) {
        alert("输入中不能含有 ' ; 等字符，已被替换为  ’ ；")
        ele.value = ele.value.replace(/[']/g, "’");
        ele.value = ele.value.replace(/[;]/g, "；");
    }
}

//预算编制自动求和
function ccltTotalBudget_pre() {
    var sum = 0;
    $('.budget_pre').each(function() {
        if ($(this).val() != '') {
            sum += parseFloat($(this).val());
        }
    })
    $('span[id$=lb_c_total_task_budget_pre]').text(sum.toFixed(2))
    cclMisBudget();
}

//预算调整自动求和
function ccltTotalBudget() {
    var sum = 0;
    $('.budget').each(function() {
        if ($(this).val() != '') {
            sum += parseFloat($(this).val());
        }
    })
    $('span[id$=lb_c_total_task_budget]').text(sum.toFixed(2))
    cclMisBudget();
}

//单击人员选择按钮
function choosePerson(btn) {
    feedbackUserName = $(btn).prev().prev();
    feedbackUserID = $(btn).prev();
    $("#hidPerson").val("person1");
    SelPersons();
}

//人员选择后单击保存按钮
function savePick() {
    var r = Save();
    var id = $("#hidPerson").val();
    if (id == "person1") {
        feedbackUserName.val(r.st_name);
        feedbackUserID.val(r.st_id);
    }
    $('#win').dialog('close');
}

//生产分工前检查是否有空值
function btn_production_divide_click() {
    if (!checkNull($('.proName'))) {
        return confirm('确定要提交吗？');
    } else {
        return false;
    }


}
//采购分工前检查是否有空值
function btn_purchase_divide_click() {
    if (!checkNull($('.purName'))) {
        return confirm('是否确认提交？');
    } else {
        return false;
    }
}

//检查传入的元素是否不为空
function checkNull(eles) {
    for (var i = 0; i < eles.length; i++) {
        if (eles[i].value == '') {
            alert('请填写完整');
            return true;
        }
    }
    return false;
}

//提交确认
function btn_confirm() {
    return confirm("是否确认提交？");
}

//计算下发与预算的各项差额
function cclMisBudget() {
    var m1, m2, m3, m4, m5
    setGreaterRed($('#span_budget_mis'), $('span[id$=lb_c_total_task_budget]').text() - $('span[id$=lb_c_total_task_budget_pre]').text());
    setGreaterRed($('#span_material_mis'), $('input[id$=txt_total_material_budget]').val() - $('input[id$=txt_total_material_budget_pre]').val());
    setGreaterRed($('#span_labour_mis'), $('input[id$=txt_labour_budget]').val() - $('input[id$=txt_labour_budget_pre]').val());
    setGreaterRed($('#span_teamwork_mis'), $('input[id$=txt_teamwork_budget]').val() - $('input[id$=txt_teamwork_budget_pre]').val());
    setGreaterRed($('#span_cooperative_mis'), $('input[id$=txt_coopreative_budget]').val() - $('input[id$=txt_coopreative_budget_pre]').val());
}

//计算采购反馈与历史参考值的差值，如果>0就标为红色
function cclMisPurchaseFB() {
    $('.puchaseFB_mis').each(function() {
        var t = $(this);
        setGreaterRed(t, t.parent().prev().prev().children("input:last-child").val() - t.parent().prev().children("span:last-child").text());
    });
    var t = $('#span_puchaseTotalFB_mis');
    console.log(t.get(0));
    setGreaterRed(t, t.parent().prev().prev().children("span:last-child").text() - t.parent().prev().children("span:last-child").text())
}

//如果值比0大，字体颜色设置成红色
function setGreaterRed(jObj, v) {
    if (isNaN(v))
        v = 0;
    if (v > 0)
        jObj.text(v.toFixed(2)).css('color', 'red');
    else
        jObj.text(v.toFixed(2));
}