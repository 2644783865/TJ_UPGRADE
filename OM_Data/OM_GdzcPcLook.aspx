<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_GdzcPcLook.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.OM_Data.OM_GdzcPcLook" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    固定资产购置&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <%--<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />--%>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function check1()
  {
  var first=document.getElementById('<%=txtshr.ClientID %>').value;
     if(first=="")
       {
         alert("请填写审核人");
          return false;
       }
  }
      function check2()
  {
  var value;
   //得到radiobuttonlist
      var vRbtid=document.getElementById("<%=rblfirst.ClientID %>");
      //得到所有radio
      var vRbtidList= vRbtid.getElementsByTagName("INPUT");
      for(var i = 0;i<vRbtidList.length;i++)
      {
        if(vRbtidList[i].checked)
        {
        //   var text =vRbtid.cells[i].innerText;
           value=vRbtidList[i].value;
//           alert("选中项的text值为"+text+",value值为"+value);
        }
      }
  var second=document.getElementById('<%=txt_second.ClientID %>').value;
  if(value=="2")
  {
  if(second=="")
       {
         alert("请填写总经理审核人");
         return false;
       }
  }
  }
  //**********弹出技术部人员子窗口***********************
    var i;
    array=new Array();
    function SelTechPersons1()
    {
      $("#hidPerson").val("first");
            SelPersons();
    }
    function SelTechPersons2()
    {
      $("#hidPerson").val("second");
            SelPersons();
    }  
       //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txtshr.ClientID %>").val(r.st_name);
                $("#<%=shrid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }


        function amountrow(obj) {
            var table = document.getElementById("ctl00_PrimaryContent_TabContainer1_Tab_sqnr_GridView1");
            var tr = table.getElementsByTagName("tr");
            var num = obj.parentNode.parentNode.getElementsByTagName("td")[3].getElementsByTagName("span")[0].innerHTML;
            var dj = obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
            var zj = obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            if (dj != null) {
                zj = (dj * num).toFixed(2);
                obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value = zj;
            }
            amount();
        }
        function amount() {
            var hj = 0;
            var gv = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv.rows.length - 1); i++) {
                var var1 = gv.rows[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
                if (var1 == "") {
                    var1 = 0;
                }
                hj += parseFloat(var1);
            }
            var zje = gv.rows[gv.rows.length - 1].getElementsByTagName("td")[7].getElementsByTagName("span")[0];
            zje.innerHTML = hj.toFixed(2);
        }
    </script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSubmit" runat="server" Text="提交审批" OnClick="btnSubmit_OnClick"
                                    OnClientClick="return check1();" CssClass="button-outer" Visible="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnLoad" runat="server" Text="提 交" OnClick="btnLoad_OnClick" OnClientClick="return check2();"
                                    CssClass="button-outer" Visible="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="false" OnClick="btnReturn_OnClick"
                                    CssClass="button-outer" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
            ActiveTabIndex="0">
            <cc1:TabPanel ID="Tab_sqnr" runat="server" HeaderText="采购内容" TabIndex="0" Width="100%">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div class="box-outer">
                            <table id="Table1" align="center" cellpadding="4" cellspacing="1" runat="server"
                                class="toptable grid" border="1">
                                <tr align="center">
                                    <td align="center" colspan="6">
                                        <asp:Label ID="lbltitle1" runat="server" Text="固定资产购置申请表" Font-Bold="true" Font-Size="Large"></asp:Label>
                                        <asp:Label ID="lbltitle2" runat="server" Text="固定资产购置订单" Font-Bold="true" Font-Size="Large"
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_state" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center">
                                        采购单号：<asp:Label ID="lblCode" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_context" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        申请部门：
                                    </td>
                                    <td style="width: 15%">
                                        <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 15%">
                                        联系人：
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lblLinkman" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 15%">
                                        联系电话：
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="6">
                                        申购清单如下所示：
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="主键" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="型号或参数" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNum" runat="server" Text='<%#Eval("NUM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="放置地点" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("LOCATION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="需求时间" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblXqtime" runat="server" Text='<%#Eval("XQTIME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtUprice" runat="server" Text='<%#Eval("UPRICE") %>' Width="80px"
                                                            onkeyup="amountrow(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblhj" runat="server" Text="合计："></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="总价" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTprice" runat="server" Text='<%#Eval("TPRICE") %>' Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblzje" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 60px">
                                        申购理由：
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblReason" runat="server" Height="50px" Width="95%" TextMode="MultiLine"></asp:Label>
                                    </td>
                                    <td style="height: 60px">
                                        备注：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNote" runat="server" Height="50px" Width="95%" TextMode="MultiLine"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 40px">
                                    <td align="left">
                                        附件图片：
                                    </td>
                                    <td class="category">
                                        <%--<asp:FileUpload ID="FileUpload1" runat="server" />--%><%--<asp:Button ID="btnAddFU" runat="server"
                                        Text="上传图片" OnClick="btnUp_Click" CausesValidation="False" />
                                    <br />--%>
                                        <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="false"></asp:Label>
                                        <asp:GridView ID="AddGridViewFiles" runat="server" CellPadding="4" CssClass="toptable grid"
                                            AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                                            Width="50%">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:BoundField DataField="fileName" HeaderText="文件名称" HeaderStyle-Wrap="false">
                                                    <ControlStyle Font-Size="Small" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fileUpDate" HeaderText="文件上传时间" HeaderStyle-Wrap="false">
                                                    <ControlStyle Font-Size="Small" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <%-- <asp:TemplateField HeaderText="删除" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                                        Height="15px" Width="15px" OnClick="imgbtnDelete_Click" CausesValidation="False"
                                                        ToolTip="删除" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ControlStyle Font-Size="Small" />
                                                <%--<HeaderStyle Width="30px" />--%>
                                                <%--</asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="下载" HeaderStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                                            OnClick="imgbtnDF_Click" Height="15px" Width="15px" CausesValidation="False"
                                                            ToolTip="下载" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ControlStyle Font-Size="Small" />
                                                    <%--<HeaderStyle Width="30px" />--%>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" ForeColor="White"
                                                Height="10px" />
                                            <RowStyle BackColor="#EFF3FB" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        申请人：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAgent" runat="server"></asp:Label><asp:Label ID="lblAgent_id" runat="server"
                                            Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblshr" runat="server" Text="审核人：" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtshr" runat="server" Width="150px"></asp:TextBox>
                                        <input id="shrid" type="text" runat="server" readonly="readonly" style="display: none" />
                                        <asp:HyperLink ID="hlSelect0" runat="server" CssClass="hand" OnClick="SelTechPersons1()"
                                            Visible="false">
                                            <asp:Image ID="AddImage0" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />
                                            选择</asp:HyperLink>
                                        <span id="span1" runat="server" visible="false" class="Error">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请选择审核人！"
                                            ControlToValidate="txtshr" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        申请时间：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAddtime" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="Tab_spxx" runat="server" HeaderText="审批信息" TabIndex="1" Width="100%">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div class="box-outer">
                            <table width="100%">
                                <tr>
                                    <td style="font-size: large; text-align: center; height: 43px" colspan="2">
                                        固定资产申请审批
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="box-outer">
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center">
                                        部门领导审批
                                    </td>
                                    <td colspan="3">
                                        <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                            <tr style="height: 25px">
                                                <td align="center" style="width: 10%">
                                                    审批人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txt_first" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                                                    <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    <asp:HyperLink ID="hlSelect1" Visible="false" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                            align="absmiddle" runat="server" />
                                                        选择</asp:HyperLink>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结论
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" Enabled="false" runat="server"
                                                        Height="20px">
                                                        <asp:ListItem Text="同意" Value="2" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
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
                                                    <asp:TextBox ID="first_opinion" runat="server" Enabled="false" TextMode="MultiLine"
                                                        Width="100%" Height="42px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        总经理审批
                                    </td>
                                    <td colspan="3">
                                        <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                            <tr style="height: 25px">
                                                <td align="center" style="width: 10%">
                                                    审批人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txt_second" runat="server"  Width="80px"></asp:TextBox>
                                                    <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    <asp:HyperLink ID="hlSelect2" runat="server" Visible="false" CssClass="hand" onClick="SelTechPersons2()">
                                                        <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                            align="absmiddle" runat="server" />
                                                        选择</asp:HyperLink>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结论
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" Enabled="false" runat="server"
                                                        Height="20px">
                                                        <asp:ListItem Text="同意" Value="4" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="5"></asp:ListItem>
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
                                                    <asp:TextBox ID="second_opinion" runat="server" Enabled="false" TextMode="MultiLine"
                                                        Width="100%" Height="42px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblState" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
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
                            <input id="dep" name="dept" value="02" />
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
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">取消</a>
            <input id="hidPerson" type="hidden" value="" />
        </div>
    </div>
</asp:Content>
