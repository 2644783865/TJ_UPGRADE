<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Office_Material_List.aspx.cs"
    Inherits="ZCZJ_DPF.Basic_Data.Office_Material_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/Extentions/jeasyui.extensions.validatebox.js" type="text/javascript"></script>
</head>

<script type="text/javascript">
    //新增窗口初始化
    $(function() {

        $('#diaNew').show().dialog({
            title: '行政管理物料',
            width: 250,
            height: 270,
            closed: true,
            cache: false,
            modal: true,
            buttons: '#buttons'
        });
    });

    var pId;
    var action;
    $(function() {
        $("#tgmarid").treegrid({
            url: 'BD_AjaxHandler.aspx',
            collapsible: true,
            loadMsg: '数据加载中请稍后……',
            queryParams: {
                method: 'GetOfficeTH'
            },
            idField: "id",
            treeField: "name",
            fitColumns: true,
            resizeHandle: "both",
            collapsible: true,
            singleSelect: true,
            columns: [[
                        {
                            field: 'ck', checkbox: true, width: 30
                        },
                         {
                             field: 'maid', title: '物料编码', width: 40, align: "center"
                         },
                            {
                                field: 'name', title: '名称', width: 40, align: "center"
                            },
                               {
                                   field: 'canshu', title: '规格及型号', width: 40, align: "center"
                               },
                               {
                                   field: 'pname', title: '父类名称', width: 40, align: "center"
                               },

                {
                    field: 'price', title: '参考价格', width: 40, align: "center"
                },

                {
                    field: 'unit', title: '单位', width: 30, align: "center"
                },
                {
                    field: 'kc', title: '安全库存', width: 30, align: "center"
                },
                { field: 'state', title: '是否为底层物料',align: "center", formatter: function(value, row, index) {
                    if (value == 'open') {
                        return ' <input type="checkbox" checked="checked" />';
                    } else {
                        return ' <input type="checkbox"/>';
                    }
                }
            }, { field: 'addmannm', title: '最近维护人', align: "center" }, { field: 'addtime', title: '维护时间', align: "center" }
            ]]
        });
    });

    //增加数据
    $(function() {
        $("#btnAdd").click(function() {
            var row = $("#tgmarid").treegrid('getSelected');
            if (row == null) {
                pId = 0;
            }
            else {
                // console.log(row);
                if (row.isbottom == "on") { alert("该物料为底层材料，无法继续新增"); return; }
                else { pId = row.id; }
            }

            action = "add";
            $('#diaNew').dialog("open");
            $("#frm").form("clear");
            $("#pid").combobox('setValue', pId);
            $("#maid").val(row.maid);
        });
    });


    //修改数据
    $(function() {

        $("#btnEdit").click(function() {
            var row = $("#tgmarid").treegrid('getSelected');
            if (row == null) {
                alert("请选择一条数据");
            }
            else {
                action = "edit";
                $('#diaNew').dialog("open");
                $("#frm").form("load", row);
                console.log(row);
            }
        });
    });
    //删除数据
    $(function() {
        $("#btnRemove").click(function() {
            var row = $("#tgmarid").treegrid('getSelected');
            if (row == null) {
                alert("请选择一条数据");
            }
            else {

                $.messager.confirm('确认对话框', '您确定要删除该条数据吗？', function(r) {
                    if (r) {
                        $.ajax({
                            url: 'BD_AjaxHandler.aspx',
                            data: { method: 'RemoveOfficeTH', Id: row.id },
                            type: 'post',
                            dataType: 'text',
                            success: function(data) {
                                $.messager.show({
                                    title: '消息提示',
                                    msg: data,
                                    timeout: 5000,
                                    showType: 'slide'
                                });
                                $("#tgmarid").treegrid("reload");
                            }
                        })
                    }
                });


            }
        });

    });

    //保存
    function save() {
        //  alert($("#frm").form("validate"));
        if ($("#frm").form("validate")) {
            $.messager.progress(); // 显示进度条
            $("#frm").form('submit', {
                url: 'BD_AjaxHandler.aspx',
                success: function(data) {
                    $.messager.progress('close'); // 如果提交成功则隐藏进度条
                    $.messager.show({
                        title: '消息提示',
                        msg: data,
                        timeout: 5000,
                        showType: 'slide'
                    });
                    $('#diaNew').dialog("close");
                    $("#tgmarid").treegrid("load");
                },
                onSubmit: function(param) {
                    param.method = 'SaveOfficeTH',
                    param.action = action
                }
            });
        }
        if (action == "add") {
            $("#pid").combobox("reload");
        }
    }
</script>

<body> <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContentTop">
        <div class="RightContentTitle">
            <table width="100%">
                <tr>
                    <td width="15">
                        <asp:Image ID="Image2" AlternateText="关闭左边管理菜单" Style="cursor: hand" onClick="switchBar(this)"
                            CssClass="CloseImage" ImageUrl="~/Assets/images/bar_hide.gif" runat="server" />
                    </td>
                    <td>
                        行政物料管理
                    </td>
                    <td width="15">
                        <asp:Image ID="Image3" ImageUrl="~/Assets/images/bar_up.gif" AlternateText="隐藏" Style="cursor: hand"
                            onClick="switchTop(this)" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" >
                      <input type="button" id="btnAdd" runat ="server" value="新增" />
                <input type="button" id="btnEdit" runat ="server" value="编辑" />
                <input type="button" id="btnRemove" runat ="server" value="删除" />
               
             
               <%-- <a id="btnAdd" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:'true'">
                    新增</a> <a id="btnEdit" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:'true'">
                        编辑</a> <a id="btnRemove" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:'true'">
                            删除</a>--%>
               
             
       
            </div>
        </div>
    </div>
    <div>
        <table id="tgmarid" style="height: 400px">
        </table>
    </div>
    <div id="diaNew">
        <form method="post" id="frm">
        <table>
            <tr>
                <td>
                    父类名称:
                </td>
                <td>
                    <input id="pid" class="easyui-combobox" name="pid" data-options="valueField:'id',textField:'name',url:'BD_AjaxHandler.aspx?method=GetAllPth',panelHeight:'auto'" />
                </td>
            </tr>
            <tr>
                <td>
                    物料编码:
                </td>
                <td>
                    <input type="text" name="maid" id="maid" class="easyui-validatebox" data-options="required:true" />
                </td>
            </tr>
            <tr>
                <td>
                    名称:
                </td>
                <td>
                    <input type="text" name="name" class="easyui-validatebox" data-options="required:true" />
                    <input type="hidden" name="id" />
                </td>
            </tr>
            <tr>
                <td>
                    规格及型号:
                </td>
                <td>
                    <input type="text" name="canshu" />
                </td>
            </tr>
            <tr>
                <td>
                    参考价格:
                </td>
                <td>
                    <input type="text" name="price" class="easyui-validatebox" data-options="validType:'MoneyorNull'" />
                </td>
            </tr>
            <tr>
                <td>
                    单位:
                </td>
                <td>
                    <input type="text" name="unit" class="easyui-validatebox" data-options="required:true" />
                </td>
            </tr>
             <tr>
                <td>
                    安全库存:
                </td>
                <td>
                    <input type="text" name="kc"  />
                </td>
            </tr>
            <tr>
                <td>
                    底层物料:
                </td>
                <td>
                    <input type="checkbox" name="isbottom" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="buttons" style="text-align: right" visible="false">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="save();">
            保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#diaNew').dialog('close')">取消</a>
    </div>
</body>
</html>
