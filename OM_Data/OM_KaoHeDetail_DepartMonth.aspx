<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoHeDetail_DepartMonth.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHeDetail_DepartMonth" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    部门月度考核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        //**********弹出技术部人员子窗口***********************
        var i;
        array = new Array();
        function SelTechPersons1() {
            $("#hidPerson").val("first");
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

            $('#win').dialog('close');
        }
       

    </script>

    <script type="text/javascript">
        $(function() {
            CaculateZong();
        });


        function CaculateZong() {

            var Zonghe = 0;
            var i = 0;
            $("#gr input[name*=txtScore]").each(function() {
                var num = parseFloat($(this).val());
                if (isNaN(num)) { num = 0 }
                Zonghe += num
              //  console.log(Zonghe)
                i = i + 1;
            });

            $("#lblEverage").html((Zonghe / i).toFixed(2));

        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="float: right">
     <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <asp:Button ID="btnAudit" runat="server" Text="提交审批" OnClick="btnAudit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnsubmit" runat="server" Text="保 存" OnClientClick="return Test();"
            OnClick="btnsubmit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnBack" onclick="window.location.href='OM_KaoHeList.aspx';"
            value="返 回" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input type="hidden" id="hidId" runat="server" />
        <input type="hidden" id="hidConext" runat="server" />
        <input type="hidden" id="hidState" runat="server" />
        <input type="hidden" id="hidAction" runat="server" />
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="考核信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer" style="text-align: center;">
                        <div style="text-align: center;">
                            <h2>
                                部门月度绩效考核结果明细表</h2>
                        </div>
                        <br />
                        <asp:Panel ID="Panel0" runat="server">
                            <table width="1150px">
                                <tr>
                                    <td>
                                        制单人姓名：
                                    </td>
                                    <td>
                                        <asp:Label ID="lb1" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        制单时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTime" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                    <td>
                                        绩效考核月份：
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" Width="100px" ID="txtKhNianYue" class="easyui-datebox"
                                            data-options="formatter:function(date){var y = date.getFullYear();var m = date.getMonth()+1;return y+'-'+m;},editable:false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <div style="text-align: center; overflow: auto; height: 300px">
                            <table id="gr" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                <asp:Repeater ID="Det_Repeater" runat="server">
                                    <HeaderTemplate>
                                        <tr class="tableTitle headcolor">
                                            <td>
                                                <asp:Label ID="Label" Text="序 号" runat="server" Font-Bold="true" Width="50px"></asp:Label>
                                            </td>
                                            <td>
                                                <strong>部门</strong>
                                            </td>
                                            <td>
                                                <strong>考核分数</strong>
                                            </td>
                                            <td>
                                                <strong>备注</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                            <td>
                                                <div style="width: 50px">
                                                    <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtDepartNM" runat="server" Width="100px" Text='<%#Eval("DepartNM")  %>'></asp:Label>
                                                <input type="hidden" id="hidDepartId" runat="server" value='<%#Eval("DepartId") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtScore" name="txtScore" runat="server" Width="100px" onkeyup="CaculateZong()"
                                                    Text='<%#Eval("Score")  %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNote" runat="server" Width="250px" Height="30px" TextMode="MultiLine"
                                                    Text='<%#Eval("Note") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr runat="server" id="tr_foot">
                                    <td runat="server" id="foot" colspan="2" align="right">
                                        <strong>平均数：</strong>
                                    </td>
                                    <td>
                                        <span id="lblEverage"></span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="float: left; padding-top: 10px">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="审核信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-outer">
                    <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Panel runat="server" ID="Panel1">
                            <tr>
                                <td align="center">
                                    一级审核
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                考核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                                    <asp:Image ID="AddImage12" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList runat="server" ID="rblResult1" RepeatColumns="2">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
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
                                                <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
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
                                <input id="dep" name="dept" value="03">
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
