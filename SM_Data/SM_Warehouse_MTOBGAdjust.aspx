<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="SM_Warehouse_MTOBGAdjust.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_MTOBGAdjust" Title="技术变更MTO调整" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
     
    <script type="text/javascript" language="javascript">
        function checkNum(tb) {
            var ajdn = parseFloat(tb.value);
            var wn = parseFloat(tb.parentNode.parentNode.getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML);

            if (isNaN(ajdn)) {
                alert("请输入正确的数量！");
                tb.value = wn;
                Statistic();
            }
            if (ajdn <= 0 || ajdn > wn) {
                alert("调整数量必须大于0且不大于可调数量！");
                tb.value = wn;
                Statistic();
            }
        }

        function checkQuantity(tb) {
            var adjqn = parseFloat(tb.value);
            var wqn = parseFloat(tb.parentNode.parentNode.getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML);

            if (isNaN(adjqn)) {
                alert("请输入正确的数量！");
                tb.value = wqn;
                Statistic();
            }
            if (adjqn < 0 || adjqn > wqn) {
                alert("调整数量必须大于0且不大于可调数量！");
                tb.value = wqn;
                Statistic();
            }
        }
        
        function Statistic() {
            var tadjn = 0;
            var tadjqn = 0;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            
            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var val1 = gv1.rows[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value;
                var val2 = gv1.rows[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
                tadjn += parseFloat(val1);
                tadjn += Math.round(val2);
            }
            var lbtn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[19].getElementsByTagName("span")[0];
            lbtn.innerHTML = tadjn.toFixed(4);
            var lbtqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[20].getElementsByTagName("span")[0];
            lbtqn.innerHTML = tadjqn;
        }

        function checkPage(){
            var flag = true;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");

            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var ptcto = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].innerHTML;
                var ptcfrom = gv1.rows[i].getElementsByTagName("td")[11].getElementsByTagName("span")[0].innerHTML;

                if (ptcto == "--请选择--") {
                    gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].style.color = "#FF0000";
                    flag = false;
                }
                else {
                    gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].style.color = "#0000FF";
                }
                if (ptcto != "--请选择--") {
                    var zhto = ptcto.split("_");
                    var zhfrom = ptcfrom.split("_");
                    if (zhto[0] == zhfrom[0]) {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].style.color = "#0000FF";
                    }
                }
            }
            if (flag == false) 
            {
                alert("请正确选择目标工程！");
            }
            return flag;
        }

        function append() {
            var retVal = window.showModalDialog("SM_Warehouse_Query.aspx?FLAG=APPENDMTO","","dialogWidth=1280px;dialogHeight=800px;status=no;help=no;scroll=yes");
            return retVal;
        }

        function closewin() {
            window.close();
        }
        
        function btnPrint_onclick() {
        window.showModalDialog('SM_Warehouse_MTO_Print.aspx?mtocode=<%=LabelCode.Text %>','',"dialogWidth=800px;dialogHeight=700px");   
        }
    </script>
    
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
        
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
    <asp:Panel ID="Operation" runat="server">
    <table width="100%">
    <tr>
        <td>
            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="true"></asp:Label>
        </td>
        <td align="right">
        <asp:Button ID="Append" runat="server" Text="追加" CausesValidation="false"  OnClick="Append_Click" OnClientClick="return append()"/>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Delete" runat="server" Text="删除" CausesValidation="false" OnClick="Delete_Click"/>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Verifiy" runat="server" Text="审核" OnClick="Verify_Click" OnClientClick="return checkPage()" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="AntiVerify" runat="server" Text="反审" OnClick="AntiVerify_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="DeleteBill" runat="server" Text="删单" OnClick="DeleteBill_Click"/>&nbsp;&nbsp;&nbsp;           
        <asp:Button ID="Print" runat="server" Text="打印" OnClick="Print_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Related" runat="server" Text="关联单据" OnClick="Related_Click" />&nbsp;&nbsp;&nbsp; 
        <input id="btnPrint" runat="server" type="button" value="打印"  onclick="return btnPrint_onclick()" />&nbsp;&nbsp;&nbsp;                        
        <input id="Close" type="button" value="关闭" onclick="closewin()" />&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    </table>
    </asp:Panel>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper" >
    <div class="box-outer">
    <asp:Panel ID="HeadPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>
            <td style=" font-size:x-large; text-align:center;" colspan="5">
                技术变更MTO调整单<asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false"/>
            </td>
        </tr>
        <tr style="width:100%">
            <td style="width:25%">
                &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server" Enabled="false"></asp:Label>
                <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
            </td>
            <td style="width:25%">
                &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate" Format="yyyy-MM-dd">
                </cc1:CalendarExtender>
            </td>
            <td style="width:25%">
                &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="TextBoxComment" runat="server"></asp:TextBox>
            </td>            
            <td style="width:25%">
                目标工程：<asp:DropDownList ID="DropDownListPTCTo" runat="server" Width="200px"
                    AutoPostBack="true" OnSelectedIndexChanged="DropDownListPTCTo_SelectedIndexChanged" >
                    <asp:ListItem Value="--请选择--">--请选择--</asp:ListItem>
                    <asp:ListItem Value="备库">备库</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    </asp:Panel>
    
    <asp:Panel ID="PanelBody" runat="server"  style="overflow:auto;position:static" Width="100%" Height="350px">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  Width="100%" EmptyDataText="没有相应的数据！"
         ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.DataItemIndex+1%>' Width="15px"></asp:Label>
                    <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UniqueID")%>' Visible="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="物料代码">
                <ItemTemplate>
                    <asp:Label ID="LabelSQCODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SQCODE")%>' Visible="false"></asp:Label>
                    <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>' Width="80px"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>合计：</FooterTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="物料名称">
                <ItemTemplate>
                    <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>' Width="80px" ></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="国标">
                <ItemTemplate>
                    <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>              
            <asp:TemplateField HeaderText="材质">
                <ItemTemplate>
                    <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="规格型号">
                <ItemTemplate>
                    <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="是否定尺">
                <ItemTemplate>
                    <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="长">
                <ItemTemplate>
                    <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="宽">
                <ItemTemplate>
                    <asp:Label ID="LabelWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="批号">
                <ItemTemplate>
                    <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>' Width="100px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="从计划跟踪号">
                <ItemTemplate>
                    <asp:Label ID="LabelPTCFrom" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTCFrom")%>' Width="120px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="仓库">
                <ItemTemplate>
                    <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>' Width="60px"></asp:Label>
                    <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>' Visible="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="仓位">
                <ItemTemplate>
                    <asp:Label ID="LabelPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>' Width="60px"></asp:Label>
                    <asp:Label ID="LabelPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>' Visible="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单位">
                <ItemTemplate>
                    <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="可调数量">
                <ItemTemplate>
                    <asp:Label ID="LabelWN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WN")%>' Width="60px"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelTotalWN" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="可调张(支)数">
                <ItemTemplate>
                    <asp:Label ID="LabelWQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WQN")%>' Width="80px"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelTotalWQN" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="到计划跟踪号">
                <ItemTemplate>
                    <asp:Label ID="LabelPTCTo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTCTo")%>' Width="100px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划模式">
                <ItemTemplate>
                    <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>' Width="60px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="调整数量">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxAdjN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AdjN")%>' onblur="checkNum(this)" Width="60px"></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelTotalAdjN" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="调整张(支)数">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxAdjQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AdjQN")%>' onblur="checkQuantity(this)" Width="80px"></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelTotalAdjQN" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="订单单号">
                <ItemTemplate>
                    <asp:Label ID="LabelOrderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderID")%>' Width="80px"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxNote" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Note")%>' Width="100px"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
        </Columns>
    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
    <RowStyle BackColor="#EFF3FB" Wrap="false"/>
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" Wrap="false"/>
    </asp:GridView>
    </asp:Panel>

    
    <asp:Panel ID="FooterPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>
            <td align="left" style="width:25%;">
                &nbsp;&nbsp;&nbsp;技术员：<asp:DropDownList ID="DropDownListPlaner" runat="server">
                </asp:DropDownList>
            </td>            
            <td align="left" style="width:25%;">
                &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：<asp:DropDownList ID="DropDownListDep" runat="server">
                </asp:DropDownList>
            </td>
            <td align="left" style="width:25%;">
                &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
            </td>
            <td align="left" style="width:25%;"></td>
        </tr>
        <tr>
            <td align="left" style="width:25%;">
                &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server"></asp:Label>
                <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>
            </td>            
            <td align="left" style="width:25%;">
                审核日期：<asp:Label ID="LabelApproveDate" runat="server"></asp:Label>
            </td>
            <td align="left" style="width:25%;"></td>
            <td align="left" style="width:25%;"></td>
        </tr> 
        <tr>
            <td colspan="4"> </td>
        </tr>
    </table>
    </asp:Panel>
    </div>
    </div>


</asp:Content>
