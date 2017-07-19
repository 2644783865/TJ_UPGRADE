<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_SHUIDFSP_detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SHUIDFSP_detail"
    Title="住宿水电费审批" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    住宿水电费审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    var i;
        array = new Array();
        function SelPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
    
     //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_Shr.ClientID %>").val(r.st_name);
                $("#<%=hid_shrid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
        
       function MonthValid(){
          var txtdairu=$("#<%=txtdairumonth.ClientID%>").datebox('getValue');
          if(txtdairu){
            var txtdairuyear=txtdairu.split("-")[0];
            var txtdairumonth=txtdairu.split("-")[1];
            var now=new Date();
            var month = now.getMonth();
            var year = now.getFullYear(); 
            if(((parseInt(txtdairumonth)>(parseInt(month)+1))&&(parseInt(year)==parseInt(txtdairuyear)))||(parseInt(year)<parseInt(txtdairuyear))){
               alert("代入月份不得超过本月份上个月，如8月份只能代入7月份或7月份之前数据");
               return false;
            }
            return true;
          }
          else{
            alert("请输入代入月份");
            return false;
          }
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnsave" Visible="false" Text="保 存" OnClick="btnsave_Click"
                        OnClientClick="return MonthValid()" />
                    <asp:Button ID="btnsubmit" runat="server" Visible="false" Text="提 交" OnClientClick="return confirm('确认提交？')"
                        OnClick="btnsubmit_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnback" runat="server" Text="返 回" OnClick="btnback_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" ActiveTabIndex="0">
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="住宿水电费明细" Width="100%" TabIndex="0">
            <HeaderTemplate>
                住宿水电费明细
            </HeaderTemplate>
            <ContentTemplate>
                <div id="detail" style="width: 85%" align="center">
                    <asp:Panel ID="panDetail" runat="server" Width="98.6%" Enabled="False">
                        <table id="table1" width="100%">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 审批编号：
                                    <asp:TextBox ID="txtspbh" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    代入时间：
                                    <input type="text" id="txtdairumonth" runat="server" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;}"
                                        class="easyui-datebox" style="width: 100px" onfocus="this.blur()" disabled="disabled" /><label
                                            for="txtdairumonth" style="color: red">
                                            *请勿手动输入</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 发&nbsp; 起&nbsp;人：
                                    <asp:TextBox ID="txtcreatename" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    发起时间：
                                    <asp:TextBox ID="txtcreattime" runat="server" onfocus="this.blur()"></asp:TextBox>
                                    <asp:Label ID="lbtishi" ForeColor="Red" runat="server" Text="* 发起人点击提交按钮将待审批项发起时间更改为当前时间！"
                                        Visible="False"></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" id="tab" align="center" cellpadding="2" cellspacing="1" class=" grid "
                            border="1">
                            <asp:Repeater ID="rptsushe" runat="server" OnItemDataBound="rpt_OnItemDataBound">
                                <HeaderTemplate>
                                    <tr style="background-color: #B9D3EE;" height="30px">
                                        <td align="center">
                                            <strong>序号</strong>
                                        </td>
                                        <td align="center">
                                            <strong>宿舍号</strong>
                                        </td>
                                        <td align="center">
                                            <strong>起始日期</strong>
                                        </td>
                                        <td align="center">
                                            <strong>截止日期</strong>
                                        </td>
                                        <td align="center">
                                            <strong>需承担电费金额</strong>
                                        </td>
                                        <td align="center">
                                            <strong>需承担水费金额</strong>
                                        </td>
                                        <td align="center">
                                            <strong>姓名</strong>
                                        </td>
                                        <td align="center">
                                            <strong>人均费用</strong>
                                        </td>
                                        <td align="center">
                                            <strong>实际费用</strong>
                                        </td>
                                        <td align="center">
                                            <strong>备注</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" height="30px">
                                        <td align="center">
                                            <asp:Label ID="lbXuHao" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td id="td_IDSDF" runat="server" align="center" visible="false">
                                            <%#Eval("IDSDF")%>
                                        </td>
                                        <td id="td_ssnum" runat="server" align="center">
                                            <%#Eval("ssnum")%>
                                        </td>
                                        <td id="td_startdate" runat="server" align="center">
                                            <%#Eval("startdate")%>
                                        </td>
                                        <td id="td_enddate" runat="server" align="center">
                                            <%#Eval("enddate")%>
                                        </td>
                                        <td id="td_dianfje" runat="server" align="center">
                                            <%#Eval("dianfje")%>
                                        </td>
                                        <td id="td_shuifje" runat="server" align="center">
                                            <%#Eval("shuifje")%>
                                        </td>
                                        <td id="td_stname" runat="server" align="center">
                                            <asp:Label ID="ST_NAME" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                        </td>
                                        <td id="td_renjunfy" runat="server" align="center">
                                            <%#Eval("renjunfy")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("realmoney")%>
                                        </td>
                                        <td id="td_note" runat="server" align="center">
                                            <%#Eval("note")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="4">
                                            合计：
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_hjdf" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_hjsf" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_rjhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_realhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="palNoData" runat="server" Visible="False" ForeColor="Red" HorizontalAlign="Center">
                            没有记录!<br />
                            <br />
                        </asp:Panel>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" TabIndex="1" Width="100%" HeaderText="审核">
            <ContentTemplate>
                <div class="box-wrapper">
                    <asp:Panel ID="panShenhe" runat="server" Enabled="false">
                        <div style="height: 6px" class="box_top">
                        </div>
                        <div class="box-outer">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                        住宿水电费
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="box-outer">
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr id="yjshh" runat="server">
                                    <td colspan="3">
                                        <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                            <asp:HiddenField runat="server" ID="hid_creatstid" />
                                            <asp:HiddenField runat="server" ID="hid_shrid" />
                                            <tr>
                                                <td align="center" style="width: 10%">
                                                    审批人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txt_Shr" runat="server" Width="80px"></asp:TextBox>
                                                    <asp:HyperLink ID="hlSelect" Visible="false" runat="server" CssClass="hand" onClick="SelPersons1()">
                                                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                            align="absmiddle" runat="server" />
                                                        选择
                                                    </asp:HyperLink>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结论
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList ID="rblShyj" RepeatColumns="2" runat="server" Height="20px">
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核时间
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lbShtime" runat="server" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtShnote" runat="server" Height="42px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
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
                                <input id="dep" name="dept" />
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
