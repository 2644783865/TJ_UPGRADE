//验证输入的数字
function validateNumber(e) {
    var reg = /^\d+(.\d{1,2})?$/
    var ele = e.target;
    if (!reg.test($.trim($(ele).val()))) {
        alert('请正确输入数字')
        $(ele).val('0.00').focus();
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

    if (checkNull($('.porName'))) {
        return confirm('确定要提交吗？');
    } else {
        return false;
    }


}
//采购分工前检查是否有空值
function btn_purchase_divide_click() {
    if (checkNull($('.purName'))) {
        return confirm('是否确认提交？');
    } else {
        return false;
    }
}

//检查传入的元素是否不为空
function checkNull(ele) {
    var names = ele;
    for (var i = 0; i < names.length; i++) {
        if (names[i].value == '') {
            alert('请填写完整');
            return false;
        }
    }
    return true;
}

//提交确认
function btn_confirm() {
    return confirm("是否确认提交？");
}

//计算下发与预算的各项差额
function cclMisBudget() {
    $('#span_budget_mis').text(($('span[id$=lb_c_total_task_budget_pre]').text() - $('span[id$=lb_c_total_task_budget]').text()).toFixed(2));
    $('#span_material_mis').text(($('input[id$=txt_total_material_budget_pre]').val() - $('input[id$=txt_total_material_budget]').val()).toFixed(2));
    $('#span_labour_mis').text(($('input[id$=txt_labour_budget_pre]').val() - $('input[id$=txt_labour_budget]').val()).toFixed(2));
    $('#span_teamwork_mis').text(($('input[id$=txt_teamwork_budget_pre]').val() - $('input[id$=txt_teamwork_budget]').val()).toFixed(2));
    $('#span_cooperative_mis').text(($('input[id$=txt_coopreative_budget_pre]').val() - $('input[id$=txt_coopreative_budget]').val()).toFixed(2));
} 