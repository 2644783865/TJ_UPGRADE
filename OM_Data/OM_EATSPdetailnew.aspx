<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_EATSPdetailnew.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_EATSPdetailnew" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    用餐申请查看/审批 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
   <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        
        
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
                moneyhj += parseFloat(valje);
            }
            tab.rows[tab.rows.length - 1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML=moneyhj;
        }
    
    
        var i;
        array = new Array();
        function SelPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        
        function SelPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
        
   
        
        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" 
        ActiveTabIndex="1" >
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="用餐申请明细" TabIndex="0">
            <ContentTemplate>
            <div style="overflow: scroll;height: 500px;">
                <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Button runat="server" ID="btndelete" Text="删除" OnClick="btndelete_OnClick" Visible="false" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <asp:RadioButtonList ID="rad_yctype" runat="server" RepeatColumns="2" Enabled="false">
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
                                    <asp:TextBox ID="txt_phone" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="center">
                                    用餐时间
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tb_ycdate" Width="90px" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年,dd日" TodaysDateFormat="yyyy年MM月dd日"
                ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_ycdate">
                                        </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    用餐人数
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbycrens" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="center">
                                    用餐规格
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbycguige" runat="server" Enabled="false"></asp:TextBox>
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
                                    <asp:TextBox ID="txt_contents" runat="server" Width="80%" TextMode="MultiLine" Height="50px" Enabled="false"></asp:TextBox>
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
                          <asp:Repeater ID="rptycsqspdetail" runat="server">
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
                                        <asp:Label ID="IDdetail" runat="server" Width="150px" Text='<%#Eval("IDdetail") %>' Visible="false"></asp:Label>
                                        <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                        </asp:CheckBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt1" runat="server" Text='<%#Eval("detailthing")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt2" runat="server" Width="100px" Text='<%#Eval("detailclass") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt3" runat="server" Width="100px" name="num" Text='<%#Eval("detailnum") %>' onchange="xiaoji(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt4" runat="server" Width="100px" Text='<%#Eval("detailunit") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt5" runat="server" Width="100px" name="price" Text='<%#Eval("detailprice") %>' onchange="xiaoji(this)"></asp:TextBox>
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
             </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" TabIndex="2" Width="100%" HeaderText="审核">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Button runat="server" ID="btnSave" Text="提交" OnClick="btnSave_OnClick" Visible="false" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    用餐申请
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr id="yjshh" runat="server">
                                <td align="center">
                                    部门负责人
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" Visible="false" runat="server" CssClass="hand" onClick="SelPersons1()">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" Enabled="false" RepeatColumns="2" runat="server" Height="20px">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinion1" runat="server" Enabled="false" Height="42px" TextMode="MultiLine"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="ejshh" runat="server">
                                <td align="center">
                                    食堂
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" Visible="false" CssClass="hand" onClick="SelPersons2()">
                                                    <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Enabled="false" Height="20px">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinion2" runat="server" Enabled="false" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>  
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <div>
        <div>
            <div id="win" visible="false">
                <div>
                    <table>
                        <tr>
                            <td>
                                <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                按部门查询：
                            </td>
                            <td>
                                <input id="dep" name="dept"/>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 430px; height: 230px">
                    <table id="dg">
                    </table>
                </div>
            </div>
            <div id="buttons" style="text-align: right" visible="false">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
</asp:Content>
