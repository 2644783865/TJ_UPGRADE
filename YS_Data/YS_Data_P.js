
//网页加载完成后运行
$(function() {


    //计算各类材料预算总价合计
    CalculateLabelSumToLabel('lb_YS_FERROUS_METAL_SUBTOTAL_Price', 'lb_YS_FERROUS_METAL_TOTAL_Price', 'txt_YS_FERROUS_METAL');
    CalculateLabelSumToLabel('lb_YS_PURCHASE_PART_SUBTOTAL_Price', 'lb_YS_PURCHASE_PART_TOTAL_Price', 'txt_YS_PURCHASE_PART');
    CalculateLabelSumToLabel('lb_YS_PAINT_COATING_SUBTOTAL_Price', 'lb_YS_PAINT_COATING_TOTAL_Price', 'txt_YS_PAINT_COATING');
    CalculateLabelSumToLabel('lb_YS_ELECTRICAL_SUBTOTAL_Price', 'lb_YS_ELECTRICAL_TOTAL_Price', 'txt_YS_ELECTRICAL');
    CalculateLabelSumToLabel('lb_YS_CASTING_FORGING_COST_SUBTOTAL_Price', 'lb_YS_CASTING_FORGING_COST_TOTAL_Price', 'txt_YS_CASTING_FORGING_COST');
    CalculateLabelSumToLabel('lb_YS_OTHERMAT_COST_SUBTOTAL_Price', 'lb_YS_OTHERMAT_COST_TOTAL_Price', 'txt_YS_OTHERMAT_COST');
    //计算历史材料小计
    CalculateMaterial();
    //如果材料费为空，则将参考值——历史材料小计费的值付给材料费
    if ($('input[id$=txt_YS_MATERIAL_COST]').val() == '') {
        $('input[id$=txt_YS_MATERIAL_COST]').val($('input[id$=txt_materil_history_reference]').val());
    }
    

    //计算各类材料反馈总价合计
    CalculateTextboxSumToLabel('txt_YS_FERROUS_METAL_SUBTOTAL_Price_FB', 'lb_YS_FERROUS_METAL_TOTAL_Price_FB', 'txt_YS_FERROUS_METAL_FB');
    CalculateTextboxSumToLabel('txt_YS_PURCHASE_PART_SUBTOTAL_Price_FB', 'lb_YS_PURCHASE_PART_TOTAL_Price_FB', 'txt_YS_PURCHASE_PART_FB');
    CalculateTextboxSumToLabel('txt_YS_PAINT_COATING_SUBTOTAL_Price_FB', 'lb_YS_PAINT_COATING_TOTAL_Price_FB', 'txt_YS_PAINT_COATING_FB');
    CalculateTextboxSumToLabel('txt_YS_ELECTRICAL_SUBTOTAL_Price_FB', 'lb_YS_ELECTRICAL_TOTAL_Price_FB', 'txt_YS_ELECTRICAL_FB');
    CalculateTextboxSumToLabel('txt_YS_CASTING_FORGING_COST_SUBTOTAL_Price_FB', 'lb_YS_CASTING_FORGING_COST_TOTAL_Price_FB', 'txt_YS_CASTING_FORGING_COST_FB');
    CalculateTextboxSumToLabel('txt_YS_OTHERMAT_COST_SUBTOTAL_Price_FB', 'lb_YS_OTHERMAT_COST_TOTAL_Price_FB', 'txt_YS_OTHERMAT_COST_FB');
    //计算材料反馈小计
    CalculateMaterial_FB();




    //计算总预算、毛利润、毛利率
    CalculateTotal();

});

//计算总预算、毛利润、毛利率
function CalculateTotal() {

    if ($('input[id$=txt_YS_MATERIAL_COST]').val() == '') {
        $('input[id$=txt_YS_MATERIAL_COST]').val(0);
    }
    if ($('input[id$=txt_YS_LABOUR_COST]').val() == '') {
        $('input[id$=txt_YS_LABOUR_COST]').val(0);
    }
    if ($('input[id$=txt_YS_TRANS_COST]').val() == '') {
        $('input[id$=txt_YS_TRANS_COST]').val(0);
    }
    $('input[id$=txt_YS_TOTALCOST_ALL]').val((parseFloat($('input[id$=txt_YS_MATERIAL_COST]').val()) + parseFloat($('input[id$=txt_YS_LABOUR_COST]').val()) + parseFloat($('input[id$=txt_YS_TRANS_COST]').val())).toFixed(4));
    $('input[id$=txt_YS_PROFIT]').val((parseFloat($('input[id$=txt_YS_BUDGET_INCOME]').val()) - parseFloat($('input[id$=txt_YS_TOTALCOST_ALL]').val())).toFixed(4));
    if ($('input[id$=txt_YS_BUDGET_INCOME]').val() != 0) {
        $('input[id$=txt_YS_PROFIT_RATE]').val((parseFloat($('input[id$=txt_YS_PROFIT]').val()) * 100 / parseFloat($('input[id$=txt_YS_BUDGET_INCOME]').val())).toFixed(2));

    } else {
        $('input[id$=txt_YS_PROFIT_RATE]').val("任务号收入为0")
    }
}





$('input[id$=txt_YS_UNIT_LABOUR_COST_FB]').on('input', function() {
$('input[id$=txt_labour_dispart_reference]').val((parseFloat($(this).val()) * parseFloat($('input[id$=txt_YS_WEIGHT]').val())).toFixed(4));
})


//黑色金属反馈单价更改时，自动计当前页面算反馈总价、反馈总价合计，以及预算汇总页面的单项材料费反馈、材料费反馈小计
$('input[id$=txt_YS_FERROUS_METAL_Average_Price_FB]').on('input', function() {
    if ($(this).val() == '') {
        $(this).val('0');
    }
    $(this).parent().next().children().val((parseFloat($(this).val()) * parseFloat($(this).parent().prev().prev().prev().children().text())).toFixed(4));
    CalculateTextboxSumToLabel('txt_YS_FERROUS_METAL_SUBTOTAL_Price_FB', 'lb_YS_FERROUS_METAL_TOTAL_Price_FB', 'txt_YS_FERROUS_METAL_FB');
    CalculateMaterial_FB();
});

//外购件反馈单价更改时，自动计当前页面算反馈总价、反馈总价合计，以及预算汇总页面的单项材料费反馈、材料费反馈小计
$('input[id$=txt_YS_PURCHASE_PART_Average_Price_FB]').on('input', function() {
    if ($(this).val() == '') {
        $(this).val('0');
    }
    $(this).parent().next().children().val((parseFloat($(this).val()) * parseFloat($(this).parent().prev().prev().prev().children().text())).toFixed(4));
    CalculateTextboxSumToLabel('txt_YS_PURCHASE_PART_SUBTOTAL_Price_FB', 'lb_YS_PURCHASE_PART_TOTAL_Price_FB', 'txt_YS_PURCHASE_PART_FB');
    CalculateMaterial_FB();
});

//油漆涂料反馈单价更改时，自动计当前页面算反馈总价、反馈总价合计，以及预算汇总页面的单项材料费反馈、材料费反馈小计
$('input[id$=txt_YS_PAINT_COATING_Average_Price_FB]').on('input', function() {
    if ($(this).val() == '') {
        $(this).val('0');
    }
    $(this).parent().next().children().val((parseFloat($(this).val()) * parseFloat($(this).parent().prev().prev().prev().children().text())).toFixed(4));
    CalculateTextboxSumToLabel('txt_YS_PAINT_COATING_SUBTOTAL_Price_FB', 'lb_YS_PAINT_COATING_TOTAL_Price_FB', 'txt_YS_PAINT_COATING_FB');
    CalculateMaterial_FB();

});

//电气电料反馈单价更改时，自动计当前页面算反馈总价、反馈总价合计，以及预算汇总页面的单项材料费反馈、材料费反馈小计
$('input[id$=txt_YS_ELECTRICAL_Average_Price_FB]').on('input', function() {
    if ($(this).val() == '') {
        $(this).val('0');
    }
    $(this).parent().next().children().val((parseFloat($(this).val()) * parseFloat($(this).parent().prev().prev().prev().children().text())).toFixed(4));
    CalculateTextboxSumToLabel('txt_YS_ELECTRICAL_SUBTOTAL_Price_FB', 'lb_YS_ELECTRICAL_TOTAL_Price_FB', 'txt_YS_ELECTRICAL_FB');
    CalculateMaterial_FB();
});

//铸锻件反馈单价更改时，自动计当前页面算反馈总价、反馈总价合计，以及预算汇总页面的单项材料费反馈、材料费反馈小计
$('input[id$=txt_YS_CASTING_FORGING_COST_Average_Price_FB]').on('input', function() {
    if ($(this).val() == '') {
        $(this).val('0');
    }
    $(this).parent().next().children().val((parseFloat($(this).val()) * parseFloat($(this).parent().prev().prev().prev().children().text()) * parseFloat($(this).parent().prev().prev().prev().prev().children().text())).toFixed(4));
    CalculateTextboxSumToLabel('txt_YS_CASTING_FORGING_COST_SUBTOTAL_Price_FB', 'lb_YS_CASTING_FORGING_COST_TOTAL_Price_FB', 'txt_YS_CASTING_FORGING_COST_FB');
    CalculateMaterial_FB();
});

//其他材料反馈单价更改时，自动计当前页面算反馈总价、反馈总价合计，以及预算汇总页面的单项材料费反馈、材料费反馈小计
$('input[id$=txt_YS_OTHERMAT_COST_Average_Price_FB]').on('input', function() {
    if ($(this).val() == '') {
        $(this).val('0');
    }
    $(this).parent().next().children().val((parseFloat($(this).val()) * parseFloat($(this).parent().prev().prev().prev().children().text())).toFixed(4));
    CalculateTextboxSumToLabel('txt_YS_OTHERMAT_COST_SUBTOTAL_Price_FB', 'lb_YS_OTHERMAT_COST_TOTAL_Price_FB', 'txt_YS_OTHERMAT_COST_FB');
    CalculateMaterial_FB();
});

//遍历所有txtbox的值并求和，将结果输出到一个label中和汇总页面的单项材料费汇总中
function CalculateTextboxSumToLabel(txt_nums, lb_sum, txt_sum) {
    var sum = 0;
    $('input[id$=' + txt_nums + ']').each(function() {
        if ($(this).val() != '') {
            sum += parseFloat($(this).val());
        }
    });
    $('span[id$=' + lb_sum + ']').text(sum.toFixed(4));
    $('input[id$=' + txt_sum + ']').val(sum.toFixed(4));
}
//遍历所有label的值并求和，将结果输出到一个label中和汇总页面的单项材料费汇总中
function CalculateLabelSumToLabel(lb_nums, lb_sum, txt_sum) {
    var sum = 0;
    $('span[id$=' + lb_nums + ']').each(function() {
        if ($(this).text() != '') {
            sum += parseFloat($(this).text());
        }
    });
    $('span[id$=' + lb_sum + ']').text(sum.toFixed(4));
    $('input[id$=' + txt_sum + ']').val(sum.toFixed(4));
}

//计算材料费小计
function CalculateMaterial() {
    var a = parseFloat($('input[id$=txt_YS_FERROUS_METAL]').val());
    var b = parseFloat($('input[id$=txt_YS_PURCHASE_PART]').val());
    var c = parseFloat($('input[id$=txt_YS_PAINT_COATING]').val());
    var d = parseFloat($('input[id$=txt_YS_ELECTRICAL]').val());
    var e = parseFloat($('input[id$=txt_YS_CASTING_FORGING_COST]').val());
    var f = parseFloat($('input[id$=txt_YS_OTHERMAT_COST]').val());
    $('input[id$=txt_materil_history_reference]').val((a + b + c + d + e + f).toFixed(4));
}
//计算材料费反馈小计
function CalculateMaterial_FB() {
    var a = parseFloat($('input[id$=txt_YS_FERROUS_METAL_FB]').val());
    var b = parseFloat($('input[id$=txt_YS_PURCHASE_PART_FB]').val());
    var c = parseFloat($('input[id$=txt_YS_PAINT_COATING_FB]').val());
    var d = parseFloat($('input[id$=txt_YS_ELECTRICAL_FB]').val());
    var e = parseFloat($('input[id$=txt_YS_CASTING_FORGING_COST_FB]').val());
    var f = parseFloat($('input[id$=txt_YS_OTHERMAT_COST_FB]').val());
    $('input[id$=txt_materil_dispart_reference]').val((a + b + c + d + e + f).toFixed(4));
}

//检查输入的是否为数字，绑定到onkeypress事件
function InputNumberOnly() {
    if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 46) { event.returnValue = false; alert('请输入数字 ！'); } else { event.returnValue = true; }
}