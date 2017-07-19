<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_EATNEW.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_EATNEW" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
用餐申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript" language="javascript">
        
        
        function xiaoji(obj) {
            var xiaoji;
            var shuliang=obj.parentNode.parentNode.getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;    
            var danjia=obj.parentNode.parentNode.getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;
            if (shuliang != "" && danjia != "") {
                xiaoji = danjia * shuliang;
                obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("span")[0].innerHTML=xiaoji;
            }
            else {
                xiaoji = 0;
                obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("span")[0].innerHTML=xiaoji;
            }
            moneyall(obj);
        }
        function moneyall(obj) {
            var moneyhj = 0;
            var tab = document.getElementById("tab");
            for (i = 1; i < (tab.rows.length - 1); i++)
            {
                var valje = tab.rows[i].getElementsByTagName("td")[6].getElementsByTagName("span")[0].innerHTML;
                if(valje!="")
                {
                   moneyhj += parseFloat(valje);
                }
            }
            tab.rows[tab.rows.length - 1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML=moneyhj;
        }
</script>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
              <div style="width: 100%">
                  <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="btnsave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
                  <div>
                     <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center">
                                    单号
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtCode" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="center">
                                    申请时间
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbsqdate" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    用餐类型
                                </td>
                                <td align="center">
                                    <asp:RadioButtonList ID="rad_yctype" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text="加班餐" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="客饭" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    申请人
                                </td>
                                <td align="center">
                                    <asp:Label ID="txt_apply" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    申请人电话
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_phone" runat="server"></asp:TextBox>
                                </td>
                                <td align="center">
                                    用餐时间
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tb_ycdate" Width="150px" runat="server" class="easyui-datetimebox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    用餐人数
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbycrens" runat="server"></asp:TextBox>
                                </td>
                                <td align="center">
                                    用餐规格
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbycguige" runat="server"></asp:TextBox>
                                </td>
                                <td align="center">
                                    
                                </td>
                                <td align="center">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="1">
                                    <asp:Label ID="title" runat="server" Text="备注"></asp:Label>
                                </td>
                                <td align="center" colspan="1" style="color: Red">
                                    注：加班人员用餐须注明
                                    <br />
                                    用餐人员姓名及班组 <br />
                                    外来人员必须注明来访 <br />单位名称及客人姓名
                                </td>
                                <td align="center" colspan="4">
                                    <asp:TextBox ID="txt_contents" runat="server" Width="80%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <table width="100%">
                              <tr>
                                <td align="center" colspan="7">
                                    <asp:Label ID="Label1" runat="server" Text="饮品申请明细"></asp:Label>
                                </td>
                           </tr>
                        </table>
                        <table id="tab" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                           <asp:Repeater ID="Det_Repeater" runat="server">
                              <HeaderTemplate>
                                 <tr class="tableTitle headcolor">
                                    <td align="center">
                                        序号
                                    </td>
                                    <td align="center">
                                        名称
                                    </td>
                                    <td align="center">
                                        种类
                                    </td>
                                    <td align="center">
                                        数量
                                    </td>
                                    <td align="center">
                                        单位
                                    </td>
                                    <td align="center">
                                        单价
                                    </td>
                                    <td align="center">
                                        金额
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                        <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                        </asp:CheckBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt1" runat="server" Text='<%#Eval("detailthing")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt2" runat="server" Width="100px"
                                            Text='<%#Eval("detailclass") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt3" runat="server" Width="100px" name="num" 
                                            Text='<%#Eval("detailnum") %>' onchange="xiaoji(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt4" runat="server" Width="100px"
                                            Text='<%#Eval("detailunit") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt5" runat="server" Width="100px" name="price"
                                            Text='<%#Eval("detailprice") %>' onchange="xiaoji(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="moneyone" runat="server" Width="150px" Text='<%#Eval("detailmoney") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                               <tr>
                                <td align="right" colspan="6">
                                    合计：
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbtotal" runat="server"></asp:Label>
                                </td>
                              </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
               </div>
               <div style="float: left; padding-top: 10px">
                &nbsp; &nbsp; &nbsp;<asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click"
                    Visible="false" /></div>
</asp:Content>
