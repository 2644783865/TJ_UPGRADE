<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_CWZHIBIAODetail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_CWZHIBIAODetail" Title="财务指标明细" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
  财务指标明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
   <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
         function account(obj)
         {
             var yychengben=$("#tab tr:eq(1) input:eq(0)").val();
             var xsfeiyong=$("#tab tr:eq(1) input:eq(1)").val();
             var glfeiyong=$("#tab tr:eq(1) input:eq(2)").val();
             var cwfeiyong=$("#tab tr:eq(1) input:eq(3)").val();
             var yyshouru=$("#tab tr:eq(3) input:eq(1)").val();
             var cbfeiyonghjjs=0;
             var lrzongejs=0;
             if(yychengben==""||isNaN(yychengben)==true)
             {
                 $("#tab tr:eq(1) input:eq(0)").val("0");
             }
             if(xsfeiyong==""||isNaN(xsfeiyong)==true)
             {
                 $("#tab tr:eq(1) input:eq(1)").val("0");
             }
             if(glfeiyong==""||isNaN(glfeiyong)==true)
             {
                 $("#tab tr:eq(1) input:eq(2)").val("0");
             }
             if(cwfeiyong==""||isNaN(cwfeiyong)==true)
             {
                 $("#tab tr:eq(1) input:eq(3)").val("0");
             }
             if(yyshouru==""||isNaN(yyshouru)==true)
             {
                 $("#tab tr:eq(3) input:eq(1)").val("0");
             }
             yychengben=$("#tab tr:eq(1) input:eq(0)").val();
             xsfeiyong=$("#tab tr:eq(1) input:eq(1)").val();
             glfeiyong=$("#tab tr:eq(1) input:eq(2)").val();
             cwfeiyong=$("#tab tr:eq(1) input:eq(3)").val();
             yyshouru=$("#tab tr:eq(3) input:eq(1)").val();
             var yychengbenjs=parseFloat(yychengben);
             var xsfeiyongjs=parseFloat(xsfeiyong);
             var glfeiyongjs=parseFloat(glfeiyong);
             var cwfeiyongjs=parseFloat(cwfeiyong);
             var yyshourujs=parseFloat(yyshouru);
             cbfeiyonghjjs=yychengbenjs+xsfeiyongjs+glfeiyongjs+cwfeiyongjs;
             lrzongejs=yyshourujs-cbfeiyonghjjs;
             $("#tab tr:eq(3) input:eq(0)").val(cbfeiyonghjjs.toFixed(2));
             $("#tab tr:eq(3) input:eq(2)").val(lrzongejs.toFixed(2));
         }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="width: 100%">
              <table width="100%">
                <tr>
                    <td align="right">
                        <a id="btnsave" class="easyui-linkbutton" runat="server" onserverclick="btnsave_click">保存</a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
              </table>
     </div>
     <div>
         <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <tr>
                    <td align="center">
                        年份：
                    </td>
                    <td align="center">
                        <asp:DropDownList ID="dplYear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td align="center">
                        月份：
                    </td>
                    <td align="center">
                        <asp:DropDownList ID="dplMoth" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td align="center">
                        添加人：
                    </td>
                    <td align="center">
                        <asp:Label ID="cw_zdrname" runat="server"></asp:Label>
                    </td>
                    <td align="center">
                        制单时间：
                    </td>
                    <td align="center">
                        <asp:Label ID="cw_zdtime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        营业成本：
                    </td>
                    <td align="center">
                        <asp:TextBox ID="yychengben" runat="server" onkeyup="account(this)"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center">
                        销售费用：
                    </td>
                    <td align="center">
                        <asp:TextBox ID="xsfeiyong" runat="server" onkeyup="account(this)"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center">
                        管理费用：
                    </td>
                    <td align="center">
                        <asp:TextBox ID="glfeiyong" runat="server" onkeyup="account(this)"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center">
                        财务费用：
                    </td>
                    <td align="center">
                        <asp:TextBox ID="cwfeiyong" runat="server" onkeyup="account(this)"></asp:TextBox>&nbsp;万元
                    </td>
                </tr>
                <tr>
                    <td align="center" visible="false">
                        营业成本预算：
                    </td>
                    <td align="center" visible="false">
                        <asp:TextBox ID="yychengbenys" runat="server" ForeColor="red"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center" visible="false">
                        销售费用预算：
                    </td>
                    <td align="center" visible="false">
                        <asp:TextBox ID="xsfeiyongys" runat="server" ForeColor="red"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center" visible="false">
                        管理费用预算：
                    </td>
                    <td align="center" visible="false">
                        <asp:TextBox ID="glfeiyongys" runat="server" ForeColor="red"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center" visible="false">
                        财务费用预算：
                    </td>
                    <td align="center" visible="false">
                        <asp:TextBox ID="cwfeiyongys" runat="server" ForeColor="red"></asp:TextBox>&nbsp;万元
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        成本总额：
                    </td>
                    <td align="center">
                        <asp:TextBox ID="cbfeiyonghj" runat="server" onfocus="this.blur()"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center">
                        营业收入：
                    </td>
                    <td align="center">
                        <asp:TextBox ID="yyshouru" runat="server" onkeyup="account(this)"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center">
                        利润总额：
                    </td>
                    <td align="center">
                        <asp:TextBox ID="lrzonge" runat="server" onfocus="this.blur()"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center">
                        
                    </td>
                    <td align="center">
                        
                    </td>
                </tr>
                <tr>
                    <td align="center" visible="false">
                        成本总额预算：
                    </td>
                    <td align="center" visible="false">
                        <asp:TextBox ID="cbfeiyonghjys" ForeColor="red" runat="server"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center" visible="false">
                        营业收入预算：
                    </td>
                    <td align="center" visible="false">
                        <asp:TextBox ID="yyshouruys" ForeColor="red" runat="server"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center" visible="false">
                        利润总额预算：
                    </td>
                    <td align="center" visible="false">
                        <asp:TextBox ID="lrzongeys" ForeColor="red" runat="server"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center" visible="false">
                        
                    </td>
                    <td align="center" visible="false">
                        
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        备注：
                    </td>
                    <td align="center" colspan="5">
                        <asp:TextBox ID="cw_note" runat="server" Width="90%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                    </td>
                </tr>
            </table>
      </div>
</asp:Content>
