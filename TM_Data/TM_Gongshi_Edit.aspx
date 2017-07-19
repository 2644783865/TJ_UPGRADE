<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Gongshi_Edit.aspx.cs"
    Inherits="ZCZJ_DPF.TM_Data.TM_Gongshi_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />

    <script src="../JS/jquery/jquery-1.4.2-vsdoc.js" type="text/javascript"></script>

    <script src="../JS/jquery/ajaxfileupload.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ajaxFileUpload() {
            $("#loading").ajaxStart(function() {
                $(this).show();
            }).ajaxComplete(function() { $(this).hide(); });
            $.ajaxFileUpload({
                url: 'TM_AjaxHandler.aspx?method=UploadGSFiles',
                secureuri: false,
                fileElementId: 'fileToUpload',
                dataType: 'json',
                success: function(data, status) {
                    ControlResult(data);


                },
                error: function(data, status, e) {
                    alert("请选择文件！");
                }
            })
            return false;
        }
        function ControlResult(data) {
            $("#lblUpLoadResult").html(data.msg);
            if (data.msg.indexOf('成功') > -1) {
                $("#trFileName").empty();
                $("#trFileName").html("<td colspan='4' align='center'><a href='#'>" + data.filename + "</a></td>");
                $("#hidGuanLianTime").val(data.guanlianTime);
            }
            else {
                $("#trFileName").empty();
            }
        }
        function btnConfirm() {
            var parameter = "txtName=" + $("#txtName").val() + "&txtBANCI=" + $("#txtBANCI").val() + "&hidGuanLianTime=" + $("#hidGuanLianTime").val() + "&txtBZ=" + $("#txtBZ").val() + "&method=" + $("#method").val() + "&proId=" + $("#proId").val() + "&txtEngName=" + $("#txtEngName").val() + "&txtEngModel=" + $("#txtEngModel").val() + "&txtPartName=" + $("#txtPartName").val() + "&txtTuHao=" + $("#txtTuHao").val();
            //            alert(parameter);
            
           // txtEngName=" + $("#txtEngName").val() + "&txtEngModel=" + $("#txtEngModel").val() + "&txtPartName=" + $("#txtPartName").val() + "&txtTuHao=" + $("#txtTuHao").val() 
            $.ajax({
                url: 'TM_AjaxHandler.aspx',
                data: parameter,
                type: "POST",
                dataType: 'json',
                success: function(data) {
                    alert(data.msg);
                    window.close();
                },
                error: function() { alert("请检查网络，稍后再试！"); }
            });

        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="box-wrapper">
        <div class="box-outer">
            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <tr>
                    <td align="right">
                        <strong>工时名称:</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        <strong>工时版次:</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBANCI" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td align="right">
                        <strong>设备名称:</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEngName" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        <strong>设备型号:</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEngModel" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <strong>部件名称:</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPartName" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        <strong>图号:</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTuHao" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <strong>上传附件:</strong>
                    </td>
                    <td colspan="3">
                        <input id="fileToUpload" type="file" name="fileToUpload" style="width: 150px" />
                        <input type="button" id="buttonUpload" onclick="return ajaxFileUpload();" value="上传附件" />
                        <label id="lblUpLoadResult" visible="false" style="color: Red">
                        </label>
                        <input type="hidden" runat="server" id="hidGuanLianTime" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <strong>附件名称:</strong>
                    </td>
                    <td id="trFileName" colspan="3">
                        <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <strong>备注:</strong>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtBZ" runat="server" TextMode="MultiLine" Width="90%" Height="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <%-- <asp:Button ID="aa" runat="server" Text="确定" Width="40px" onclick="aa_Click"  />--%>
                        <input id="btnCon" type="button" style="width: 40px" onclick="return btnConfirm();"
                            value="保 存" />
                        <input id="btnCancel" type="button" value="关 闭" title="不保存数据,直接关闭窗口" onclick="window.close();" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="method" runat="server" />
            <input type="hidden" id="proId" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
