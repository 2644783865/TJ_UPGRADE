var auditjs='1';
//初始化选人窗口
$(function() {
    $('#win').show().dialog({
        title: '人员信息结构表',
        width: 451,
        height: 350,
        closed: true,
        cache: false,
        modal: true,        
        buttons: '#buttons'
    });
});
//初始化部门下拉框
$(function() {
    
    $('#dep').combobox({
        url: '../QC_Data/QC_AjaxHandler.aspx?method=InitDep',
        valueField: 'dep_code',
        textField: 'dep_name',
        onSelect: function(rec) {
            $('#dg').datagrid('load', {
                dep: rec.dep_code,
                auditjs: auditjs,
                method: 'InitDepPeo'
            });
        }
    });
    
});

//选择人员，弹出对话框
function SelPersons() {
    //为部门赋值
    $(function() {
        $.post("../OM_Data/OM_AjaxHandler.aspx", { method: "FindDepbySTId" }, function(data, textStatus) {
            //alert(data.dep);
            auditjs='1';
            $("#dep").combobox('setValue', data.dep);
            var depID = $("#dep").combobox('getValue')
            $("#win").dialog("open");
            $('#dg').datagrid({
                url: '../QC_Data/QC_AjaxHandler.aspx',
                striped: true,
                fit: true,
                rownumbers: true,
                pageNumber: 1,
                pageSize: 100,
                pageList: [10, 20, 50, 100],
                columns: [[
        { checkbox: true },
        { field: 'st_name', title: '姓名', width: 60, align: 'center' },
        { field: 'st_gender', title: '性别', width: 50, align: 'center' },
        { field: 'dep_name', title: '部门名称', align: 'center', width: 150},
        { field: 'dep_position', title: '岗位名称', align: 'center', width: 100 },
        { field: 'st_id', align: 'center', hidden: true }

    ]],
                singleSelect: true,
                pagination: true,
                queryParams: {
                    dep: depID,
                    auditjs:'1',
                    method: 'InitDepPeo'
                }

            });


        }, "json");

    });


}


//二级审核人
function Sel2() {
    //为部门赋值
    $(function() {
        $.post("../OM_Data/OM_AjaxHandler.aspx", { method: "FindDepbySTId" }, function(data, textStatus) {
            //alert(data.dep);
            auditjs='2';
            $("#dep").combobox('setValue', data.dep);
            var depID = $("#dep").combobox('getValue')
            $("#win").dialog("open");
            $('#dg').datagrid({
                url: '../QC_Data/QC_AjaxHandler.aspx',
                striped: true,
                fit: true,
                rownumbers: true,
                pageNumber: 1,
                pageSize: 100,
                pageList: [10, 20, 50, 100],
                columns: [[
        { checkbox: true },
        { field: 'st_name', title: '姓名', width: 60, align: 'center' },
        { field: 'st_gender', title: '性别', width: 50, align: 'center' },
        { field: 'dep_name', title: '部门名称', align: 'center', width: 150 },
        { field: 'dep_position', title: '岗位名称', align: 'center', width: 100 },
        { field: 'st_id', align: 'center', hidden: true }

    ]],
                singleSelect: true,
                pagination: true,
                queryParams: {
                    dep: depID,
                    auditjs:'2',
                    method: 'InitDepPeo'
                }

            });


        }, "json");

    });


}




//三级审核人
function Sel3() {
    //为部门赋值
    $(function() {
        $.post("../OM_Data/OM_AjaxHandler.aspx", { method: "FindDepbySTId" }, function(data, textStatus) {
            //alert(data.dep);
            auditjs='3';
            $("#dep").combobox('setValue', data.dep);
            var depID = $("#dep").combobox('getValue')
            $("#win").dialog("open");
            $('#dg').datagrid({
                url: '../QC_Data/QC_AjaxHandler.aspx',
                striped: true,
                fit: true,
                rownumbers: true,
                pageNumber: 1,
                pageSize: 100,
                pageList: [10, 20, 50, 100],
                columns: [[
        { checkbox: true },
        { field: 'st_name', title: '姓名', width: 60, align: 'center' },
        { field: 'st_gender', title: '性别', width: 50, align: 'center' },
        { field: 'dep_name', title: '部门名称', align: 'center', width: 150 },
        { field: 'dep_position', title: '岗位名称', align: 'center', width: 100 },
        { field: 'st_id', align: 'center', hidden: true }

    ]],
                singleSelect: true,
                pagination: true,
                queryParams: {
                    dep: depID,
                    auditjs:'3',
                    method: 'InitDepPeo'
                }

            });


        }, "json");

    });


}






//点击保存，返回数据
function Save() {
    var r = $('#dg').datagrid('getSelected');
    if (!r) {
        $.messager.show({
            title: '提示消息',
            msg: '请勾选至少一行',
            timeout: 5000
        });
    }
    else {
        return r;
    }
}